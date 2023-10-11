namespace Fashion.Models
{
    public class ProductImage
    {
        public int ImageID { get; set; }
        public string ImageUrl { get; set; }
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}
