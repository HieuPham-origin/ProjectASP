using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionSales.Models
{
    public class Orders
    {
        public int orderID { get; set; }
        public bool orderStatus { get; set; }
        public DateTime orderDay { get; set; }
        public DateTime receiveDay { get; set; }
        public int customerID { get; set; }
    }
}