using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Models
{
    public class CategoryModel : AbstractModel
    {
        public int? ID { get ; set ; }

        public string Name { get ; set ; }

        public string Discription { get ; set ; }
        
    }
}