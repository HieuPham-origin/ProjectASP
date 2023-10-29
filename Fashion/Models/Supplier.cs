using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
