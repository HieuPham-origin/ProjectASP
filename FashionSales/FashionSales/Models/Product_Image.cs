using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionSales.Models
{
    public class Product_Image
    {
        public int image_ID { get; set; }
        public string imageUrl { get; set; }
        public int productID { get; set; }
    }
}