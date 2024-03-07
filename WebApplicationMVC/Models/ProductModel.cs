using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Models
{
    public class ProductModel : AbstractModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public int? StockLevel { get; set; }

        public int? CtID { get; set; }

        public int? SupID { get; set; }

        public int? IsLive { get; set; }

        public decimal? Price { get; set; }

        public int? Size { get; set; }

        public string ImgUrl { get; set; }

        public bool? IsLivebool { get; set; }

        public string catName { get; set; }

    }
}