namespace Fashion.Models
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public List<Brand> Brands { get; set; }
    }
}
