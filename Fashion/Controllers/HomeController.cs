using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fashion.Controllers
{
    public class HomeController : Controller
    {

		private readonly FashionShopContext _db;
		
		public HomeController(FashionShopContext db)
		{
			_db = db;
		}

		public IActionResult Index()
        {

            return View();
        }


        public IActionResult Shop(int page = 1, int? categoryId = null, int? brandId = null)
        {
            int pageSize = 12;
            int skip = (page - 1) * pageSize;

            var query = _db.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.categoryID == categoryId.Value);
            }

            if(brandId.HasValue)
            {
                query = query.Where(p => p.brandID == brandId.Value);
            }

            var products = query
                    .OrderBy(p => p.ProductID)
                    .Skip(skip)
                    .Take(pageSize)
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .ToList();

            var categories = _db.Categories
                .Include(c => c.Products)
                .Select(c => new CategoryViewModel
                {
                    CategoryID = c.categoryID,
                    CategoryName = c.categoryName,
                    ProductCount = c.Products.Count()
                })
                .ToList();

            var brands = _db.Brands.ToList();

            int totalProducts = _db.Products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = categories,
                Brands = brands
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var customer = _db.Customers.Find(int.Parse(customerId));
            var product = _db.Products.Find(productId);

            var order = _db.Orders.FirstOrDefault(o => o.customerID == customer.CustomerID && !o.isChecked);
            if (order == null)
            {
                order = new Order
                {
                    customerID = customer.CustomerID,
                    orderStatus = false,
                    orderDay = DateTime.Now.Day,
                    receiveDay = DateTime.Now.Day + 5,
                    isChecked = false
                };
                _db.Orders.Add(order);
                _db.SaveChanges();
            }

            var orderDetail = _db.OrderDetails.FirstOrDefault(od => od.orderID == order.orderID && od.productID == product.ProductID);
            if (orderDetail == null)
            {
                // If the product is not in the order details, add a new OrderDetail
                orderDetail = new OrderDetail
                {
                    orderID = order.orderID,
                    productID = product.ProductID,
                    quantity = 1,
                    price = product.price
                };
                _db.OrderDetails.Add(orderDetail);
            }
            else
            {
                // If the product is already in the order details, increase the quantity
                orderDetail.quantity += 1;
                orderDetail.price *= orderDetail.quantity;
            }

            _db.SaveChanges();

            return RedirectToAction("Shop");
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