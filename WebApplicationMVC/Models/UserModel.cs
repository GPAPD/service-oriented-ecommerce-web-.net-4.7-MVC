using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Models
{
    public class UserModel : AbstractModel
    {
        public int? ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? UserLevel { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<CategoryModel> category { get; set; }

        public CategoryModel categoryItem { get; set; }

        public List<ProductModel> itemList { get; set; }

        public ProductModel item { get; set; }

        public ShippingModel invoice { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? Quantity { get; set; }

        public bool? Preorder { get; set; }

        public List<OrderDetailsModel> OrderList { get; set; }

        public OrderDetailsModel Order { get; set; }

        public PreOrderModel preOrder { get; set; }


    }
}