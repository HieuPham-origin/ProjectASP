using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fashion.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

            
        public IActionResult Shop()
        {
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }
    }
}