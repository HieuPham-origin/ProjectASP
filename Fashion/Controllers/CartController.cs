using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashion.Controllers
{
    public class CartController : Controller
    {


        private readonly FashionShopContext _db;

        public CartController(FashionShopContext db)
        {
            _db = db;
        }


        public IActionResult ShoppingCart()
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var orderDetails = _db.OrderDetails
                                .Include(od => od.Product)
                                .Include(od => od.Size) // Bao gồm thông tin về Size
                                .Where(od => od.Order.CustomerID == int.Parse(customerId) && !od.Order.IsChecked)
                                .ToList();

            var productSizes = _db.ProductSizes
                                .Include(ps => ps.Product)
                                .Include(ps => ps.Size)
                                .Where(ps => orderDetails.Any(od => od.ProductID == ps.ProductID))
                                .ToList();

            var viewModel = new ShoppingCartViewModel
            {
                OrderDetails = orderDetails,
                ProductSizes = productSizes
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult UpdateCart(List<int> productIds, List<int> orderIds, List<int> quantities, List<int> sizeIds)
        {
            for (int i = 0; i < productIds.Count; i++)
            {
                int productId = productIds[i];
                int orderId = orderIds[i];
                int quantity = quantities[i];
                int sizeId = sizeIds[i];

                var orderDetail = _db.OrderDetails.FirstOrDefault(od => od.ProductID == productId && od.OrderID == orderId);
                if (orderDetail != null)
                {
                    // Kiểm tra xem `sizeId` có giá trị hợp lệ hay không
                    var validSize = _db.Sizes.FirstOrDefault(s => s.SizeID == sizeId);
                    if (validSize != null)
                    {
                        orderDetail.Quantity = quantity;
                        orderDetail.SizeID = sizeId;
                        _db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("ShoppingCart");
        }


        public IActionResult Checkout()
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var customer = _db.Customers.FirstOrDefault(c => c.CustomerID == int.Parse(customerId));
            if (customer == null)
            {
                return RedirectToAction("Login", "User");
            }

            var orderDetails = _db.OrderDetails
                                  .Include(od => od.Product)
                                  .Where(od => od.Order.CustomerID == int.Parse(customerId) && !od.Order.IsChecked)
                                  .ToList();

            var viewModel = new CheckoutViewModel
            {
                Customer = customer,
                OrderDetails = orderDetails
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Checkout(string address)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var customer = _db.Customers.FirstOrDefault(c => c.CustomerID == int.Parse(customerId));
            if (customer == null)
            {
                return RedirectToAction("Login", "User");
            }

            customer.Address = address;
            _db.SaveChanges();

            var orders = _db.Orders.Where(o => o.CustomerID == customer.CustomerID && !o.IsChecked).ToList();
            foreach (var order in orders)
            {
                order.IsChecked = true;
            }
            _db.SaveChanges();

            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

    }
}
