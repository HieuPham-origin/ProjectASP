using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public string categoryName { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
