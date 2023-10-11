using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Favorite_Product
    {
        [Key, Column(Order = 0)]
        public int productID { get; set; }
        [Key, Column(Order = 1)]
        public int customerID { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
