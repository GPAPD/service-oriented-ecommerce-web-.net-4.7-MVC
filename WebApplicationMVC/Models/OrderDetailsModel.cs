using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMVC.Models
{
    public class OrderDetailsModel
    {
        public int? ID { get; set; }

        public int? OrderID { get; set; }

        public int? ItemID { get; set; }

        public string CsName { get; set; }

        public string CsLast { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public int? SuppID { get; set; }

        public int? ItemCount { get; set; }

        public string OrderStatues { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal? Total { get; set; }
    }
}