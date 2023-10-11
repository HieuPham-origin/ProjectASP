using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public int password { get; set; }
        public string address { get; set; }

        // Navigation properties
        public virtual ICollection<Favorite_Product> Favorite_Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
