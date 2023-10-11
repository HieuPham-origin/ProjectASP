using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        public int productID { get; set; }
        [Key, Column(Order = 1)]
        public int orderID { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}