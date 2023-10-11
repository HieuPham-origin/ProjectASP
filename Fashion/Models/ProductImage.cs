using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class ProductImage
    {
        [Key]
        public int imageID { get; set; }
        public string imageUrl { get; set; }
        public int productID { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
    }
}
