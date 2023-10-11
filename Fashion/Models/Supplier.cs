using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        [Required]
        public string supplierName { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string address { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
