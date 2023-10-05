using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionSales.Models
{
    public class Orders_Detail
    {
        public int quantity { get; set; }
        public int price { get; set; }
        public int productID { get; set; }
        public int orderID { get; set; }
    }
}