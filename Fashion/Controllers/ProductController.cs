using Fashion.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashion.Controllers
{
    public class ProductController : Controller
    {

        private readonly FashionShopContext _db;

        public ProductController(FashionShopContext db)
        {
            _db = db;
        }
        public IActionResult Product_Detail(int id)
        {
            var product = _db.Products
                 .Include(p => p.ProductImages)
                 .Include(p => p.Category)
                 .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }


            return View(product);
        }
    }
}
