using Microsoft.AspNetCore.Mvc;

namespace Fashion.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Sales()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }
        public IActionResult Invoice_Details()
        {
            return View();
        }
    }
}
