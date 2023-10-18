using System.ComponentModel.DataAnnotations;

namespace Fashion.Models
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }
        public bool orderStatus { get; set; }
        public int orderDay { get; set; }
        public int receiveDay { get; set; }
        public int customerID { get; set; }

        public bool isChecked { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
