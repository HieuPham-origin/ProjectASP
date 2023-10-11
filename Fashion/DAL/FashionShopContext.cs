using Fashion.Models;
using Microsoft.EntityFrameworkCore;

namespace Fashion.DAL
{
    public class FashionShopContext : DbContext
    {
        public FashionShopContext(DbContextOptions<FashionShopContext> options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<OrderDetail> Orders_Details { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
