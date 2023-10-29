using Fashion.DAL;
using Fashion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Fashion.Controllers
{
    public class AdminController : Controller
    {
        private readonly FashionShopContext _db;
        public AdminController(FashionShopContext db)
        {
            _db = db;
        }

        public IActionResult Sales()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            var orders = _db.Orders.Include(o => o.Customer).ToList();
            return View(orders);
        }
        [HttpPost]
        public IActionResult CheckedOrder(int orderId)
        {
            var order = _db.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = true;

            var orderDetails = _db.OrderDetails.Where(od => od.OrderID == orderId).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Price = 0;
            }

            _db.SaveChanges();

            return RedirectToAction("Invoice");
        }
        [HttpPost]
        public IActionResult Invoice_Details(int customerID)
        {
            var order = _db.Orders.Include(o => o.Customer).FirstOrDefault(o => o.CustomerID == customerID);
            if (order == null)
            {
                return RedirectToAction("Invoice");
            }

            var orderDetails = _db.OrderDetails.Include(od => od.Product).Where(od => od.OrderID == order.OrderID).ToList();
            ViewBag.OrderDetails = orderDetails;

            return View(order);
        }

        public IActionResult Customer()
        {
            var customers = _db.Customers.ToList();
            return View(customers);
        }
        public async Task<IActionResult> Customer_Detail(int id)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        public async Task<IActionResult> Product_Detail(int id)
        {
            var product = await _db.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(c => c.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction("Product"); // Chuyển hướng người dùng đến trang hoặc hành động khác sau khi xóa thành công
        }
        public IActionResult AddProduct()
        {
            ViewBag.Brands = _db.Brands.ToList(); // Lấy danh sách các thương hiệu từ cơ sở dữ liệu
            ViewBag.Categories = _db.Categories.ToList(); // Lấy danh sách các danh mục từ cơ sở dữ liệu
            //ViewBag.Supplies = _db.Suppliers.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Product"); // Chuyển hướng đến trang chủ hoặc trang sản phẩm
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel model)
        {
            // Kiểm tra xem sản phẩm có tồn tại trong cơ sở dữ liệu hay không
            var product = await _db.Products.FindAsync(model.Product.ProductID);

            if (product == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin sản phẩm
            product.ProductName = model.Product.ProductName;
            product.ProductDescription = model.Product.ProductDescription;

            // Cập nhật thông tin liên quan đến danh mục và thương hiệu
            var category = await _db.Categories.FindAsync(model.Product.CategoryID);
            if (category != null)
            {
                product.Category = category;
            }

            var brand = await _db.Brands.FindAsync(model.Product.BrandID);
            if (brand != null)
            {
                product.Brand = brand;
            }

            product.Price = model.Product.Price;
            product.Quantity = model.Product.Quantity;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _db.SaveChangesAsync();

            // Chuyển hướng người dùng đến trang hoặc hành động khác sau khi cập nhật thành công
            return RedirectToAction("Product");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer model)
        {
            if (string.IsNullOrEmpty(model.Role) &&
                model.Orders == null &&
                model.Favorite_Products == null)
            {
                // Tìm khách hàng trong cơ sở dữ liệu
                var customer = await _db.Customers.FindAsync(model.CustomerID);

                if (customer == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin khách hàng
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Phone = model.Phone;
                customer.NormalizedEmail = model.NormalizedEmail;
                customer.Address = model.Address;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _db.SaveChangesAsync();

                // Chuyển hướng người dùng đến trang hoặc hành động khác sau khi cập nhật thành công
                return RedirectToAction("Customer");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị form cập nhật lại cho người dùng
            return View("Customer_Detail", model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();

            return RedirectToAction("Customer"); // Chuyển hướng người dùng đến trang hoặc hành động khác sau khi xóa thành công
        }
        public IActionResult Product(int page = 1, int? categoryId = null, int? brandId = null)
        {
            int pageSize = 12;
            int skip = (page - 1) * pageSize;

            var query = _db.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandID == brandId.Value);
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
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    ProductCount = c.Products.Count()
                })
                .ToList();

            var brands = _db.Brands.ToList();

            int totalProducts = _db.Products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            var productImages = products.SelectMany(p => p.ProductImages).ToList();
            var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = categories,
                Brands = brands,
                ProductImages = productImages
            };

            return View(viewModel);
        }
    }
}
