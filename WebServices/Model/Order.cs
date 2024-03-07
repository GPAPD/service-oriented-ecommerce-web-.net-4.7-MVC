using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Model
{
    public class Order
    {
        public int? ID { get; set; }

        public decimal Total { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderStatues { get; set; }
    }
}