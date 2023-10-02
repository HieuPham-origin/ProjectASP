using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionSales.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Sales()
        {
            return View();
        }
    }
}