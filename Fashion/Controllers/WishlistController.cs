using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashion.Controllers
{
    public class WishlistController : Controller
    {
        private readonly FashionShopContext _db;

        public WishlistController(FashionShopContext db)
        {
            _db = db;
        }

        public IActionResult ShoppingWishlist()
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "User");
            }
            var viewModel = new WishlistViewModel
            {
                CustomerId = int.Parse(customerId),
                Products = _db.Favorite_Products
                            .Where(fp => fp.customerID == int.Parse(customerId))
                            .Select(fp => fp.Product)
                            .ToList()
            };

            return View(viewModel);
        }
    }
}
