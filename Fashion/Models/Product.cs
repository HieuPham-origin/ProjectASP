using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int BrandID { get; set; }
        public int SupplierID { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
