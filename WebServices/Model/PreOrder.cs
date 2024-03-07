using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Model
{
    public class PreOrder
    {
        public int? ID { get; set; }

        public int? ItemID { get; set; }

        public int? OrderID { get; set; }

        public int? SupID { get; set; }

        public string Statues { get; set; }

        public int? tempItemCount { get; set; }
    }
}