using System.Collections.Generic;
namespace Fashion.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
    }
}
