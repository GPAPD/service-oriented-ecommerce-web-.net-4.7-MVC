using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC.Models;
using WebApplicationMVC.webService;

namespace WebApplicationMVC.Controllers
{
    public class MyOrdersController : Controller
    {
        webService.WebService1 service = new webService.WebService1();

        // GET: MyOrders
        public ActionResult Index()
        {
            UserModel model = new UserModel();

            List<OrderDetailsModel> od = new List<OrderDetailsModel>();

            model.OrderList = od;

            return View("MyOrders", model);
        }

        public ActionResult SearchMyOrders(int OrderId = 0, string Email = null)
        {
            
            UserModel model = new UserModel();

            //List<OrderDetailsWithDateTime> orderList = service.GetOrderDetailsFromOrderIdOrEmail(id, email);

            if (OrderId > 0)
            {
                model.OrderList = service.GetOrderDetailsFromOrderIdOrEmail(OrderId, Email).Select(od => new OrderDetailsModel { ID = od.ID, OrderID = od.OrderID, ItemID = od.ItemID, CsName = od.CsName, CsLast = od.CsLast, AddressLine1 = od.AddressLine1, AddressLine2 = od.AddressLine2, City = od.City, Contact = od.Contact, Email = od.Email, SuppID = od.SuppID, ItemCount = od.ItemCount, OrderStatues = od.OrderStatues, OrderDate = od.OrderDate, Total = od.Total }).ToList();
            }
            else 
            {
                model.OrderList = service.GetOrderDetailsFromOrderIdOrEmail(null, Email).Select(od => new OrderDetailsModel { ID = od.ID, OrderID = od.OrderID, ItemID = od.ItemID, CsName = od.CsName, CsLast = od.CsLast, AddressLine1 = od.AddressLine1, AddressLine2 = od.AddressLine2, City = od.City, Contact = od.Contact, Email = od.Email, SuppID = od.SuppID, ItemCount = od.ItemCount, OrderStatues = od.OrderStatues, OrderDate = od.OrderDate, Total = od.Total }).ToList();

            }

            return View("MyOrders", model);
        }
    }
}