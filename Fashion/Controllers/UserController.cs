using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fashion.Controllers
{
    public class UserController : Controller
    {

		private readonly FashionShopContext _db;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<Customer> _userManager;
        public UserController(FashionShopContext db, IEmailSender emailSender, UserManager<Customer> userManager)
		{
			_db = db;
            _emailSender = emailSender;
            _userManager = userManager;

        }
        public IActionResult Login()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) && _db.Customers.FirstOrDefault(c => c.Email == email) != null)
			{
				Customer customer = _db.Customers.FirstOrDefault(c => c.Email == email);
				if (BCrypt.Net.BCrypt.Verify(password, customer.password))
				{
					HttpContext.Session.SetString("CustomerId", customer.CustomerID.ToString());
					HttpContext.Session.SetString("CustomerEmail", customer.Email);
					HttpContext.Session.SetString("CustomerLastName", customer.lastName);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.ThongBao = "Wrong username or password";
				}
			}

			return View();
		}


		public IActionResult Favorites() 
        {
            return View();
        }


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(CustomerRegistrationModel model)
		{
			if (ModelState.IsValid)
			{
				string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
				var customer = new Customer
				{
					firstName = model.FirstName,
					lastName = model.LastName,
					phone = model.Phone,
					Email = model.Email,
					role = "user",
					password = hashedPassword,
					address = ""
				};

				_db.Customers.Add(customer);
				_db.SaveChanges();

				return RedirectToAction("Login");
			}

			return View(model);
		}
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "User");
        }


		public IActionResult ForgotPassword() 
		{
			return View();			
		}



        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }

			string e = email;
            var user = await _db.Customers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return View("ForgotPasswordConfirmation");
            }



            // Generate the token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "User", new { token = token, email = email }, Request.Scheme);

            // Send the email
            await _emailSender.SendEmailAsync(
                email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return View("ForgotPasswordConfirmation");	
        }

        public IActionResult ResetPassword()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> ResetPassword( [FromQuery] string email)
        {
            Console.WriteLine(email);

            return View();
/*            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }*/
        }



        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }






    }
}
