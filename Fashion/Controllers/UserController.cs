using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
namespace Fashion.Controllers
{
    public class UserController : Controller
    {

		private readonly FashionShopContext _db;

		public UserController(FashionShopContext db)
		{
			_db = db;
		}
		public IActionResult Login()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) && _db.Customers.FirstOrDefault(c => c.email == email) != null)
			{
				Customer customer = _db.Customers.FirstOrDefault(c => c.email == email);
				if (BCrypt.Net.BCrypt.Verify(password, customer.password))
				{
					HttpContext.Session.SetString("CustomerId", customer.CustomerID.ToString());
					HttpContext.Session.SetString("CustomerEmail", customer.email);
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
					phoneNumber = model.Phone,
					email = model.Email,
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


    }
}
