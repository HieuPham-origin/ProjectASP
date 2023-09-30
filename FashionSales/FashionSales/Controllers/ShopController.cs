using Microsoft.AspNetCore.Mvc;

namespace FashionSales.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
