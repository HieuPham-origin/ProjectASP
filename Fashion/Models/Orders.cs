namespace Fashion.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public bool OrderStatus { get; set; }
        public int CustomerID { get; set; }

        public virtual Customer Customers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
