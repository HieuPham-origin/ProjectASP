using System.Collections.Generic;
using Fashion.Models;

namespace Fashion.Models
{
    public class WishlistViewModel
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}