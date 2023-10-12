using Microsoft.AspNetCore.Mvc;

namespace Fashion.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product_Detail()
        {
            return View();
        }
    }
}
