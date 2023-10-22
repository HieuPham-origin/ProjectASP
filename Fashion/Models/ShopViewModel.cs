namespace Fashion.Models
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public string Search { get; set; }
    }
}
