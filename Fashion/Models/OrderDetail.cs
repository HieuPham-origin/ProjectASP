namespace Fashion.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public DateTime OrderDay { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Product Product { get; set; }
    }
}