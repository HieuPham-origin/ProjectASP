using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Brand
    {
        [Key]
        public int brandID { get; set; }
        public string brandName { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
