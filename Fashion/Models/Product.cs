using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int categoryID { get; set; }
        public int brandID { get; set; }
        public int supplierID { get; set; }
        [Required]
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
