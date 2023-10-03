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

        public ActionResult Invoice()
        {
            return View();
        }
        public ActionResult Invoice_Details()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult Products_Details()
        {
            return View();
        }
        public ActionResult Product_List()
        {
            return View();
        }
    }
}