using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Customer : IdentityUser
    {
        [Key]
        public int CustomerID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }

        [Required]
        public string role { get; set; }
        [Required]
        public string password { get; set; }
        public string address { get; set; }

        public virtual ICollection<Favorite_Product> Favorite_Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
