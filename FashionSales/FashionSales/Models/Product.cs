using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionSales.Models
{
    public class Product
    {
        public int productID { get; set; }
        public int categoryID {  get; set; }
        public int brandID { get; set; }
        public int supplierID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
    }
}