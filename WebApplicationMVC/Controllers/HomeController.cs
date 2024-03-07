using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;
using WebApplicationMVC.Models;
using WebApplicationMVC.webService;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : Controller
    {
        webService.WebService1 service = new webService.WebService1();

        public ActionResult Index()
        {
            UserModel model = new UserModel();

            model.itemList = service.GetAllProducts().Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();



            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ItemDetailView(int id = 0)
        {
            UserModel model = new UserModel();
            CategoryModel ct = new CategoryModel();
            ProductModel pd = new ProductModel();

            try
            {
                // Fetch the product information with a single call to the service
                Product product = service.GetProctByProductId(id);

                // Check if the product exists
                if (product != null)
                {
                    pd.ID = product.ID;
                    pd.Name = product.Name;
                    pd.Discription = product.Discription;
                    pd.StockLevel = product.StockLevel;
                    pd.CtID = product.CtID;
                    pd.SupID = product.SupID;
                    pd.IsLive = product.IsLive;
                    pd.Price = product.Price;
                    pd.Size = product.Size;
                    pd.ImgUrl = product.ImgUrl;

                    // Fetch the category information with a single call to the service
                    Category category = service.GetCategoryByCatId((int)pd.CtID);

                    if (category != null)
                    {
                        ct.ID = category.ID;
                        ct.Name = category.Name;
                        ct.Discription = category.Discription;
                    }

                    User user = service.GetUserByUsersID((int)pd.SupID);

                    if (user != null)
                    {
                        model.ID = user.ID;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;

                    }
                }
            }
            catch (Exception e) { }

            model.item = pd;
            model.categoryItem = ct;

            return View("DetailView", model);
        }

        public ActionResult ShippingDetailPage(int id = 0, int Quantity = 0, bool Preorder = false)
        {
            UserModel model = new UserModel();
            ProductModel pd = new ProductModel();

            CategoryModel ct = new CategoryModel();

            Product product = service.GetProctByProductId(id);

            if (product != null)
            {
                pd.ID = product.ID;
                pd.Name = product.Name;
                pd.Discription = product.Discription;
                pd.StockLevel = product.StockLevel;
                pd.CtID = product.CtID;
                pd.SupID = product.SupID;
                pd.IsLive = product.IsLive;
                pd.Price = product.Price;
                pd.Size = product.Size;
                pd.ImgUrl = product.ImgUrl;

            }

            model.item = pd;

            if (id > 0 && Quantity > 0)
            {
                model.TotalPrice = (int)pd.Price * Quantity;

               

                model.Quantity = Quantity;

            }

            if (Quantity == 0 ||( Quantity > model.item.StockLevel && Preorder == false))
            {
                model.Errors.Add("Cant Procces the request because not enough Stock");
                                                                                                                                         
                try
                {
                    // Fetch the category information with a single call to the service
                    Category category = service.GetCategoryByCatId((int)pd.CtID);

                    if (category != null)
                    {
                        ct.ID = category.ID;
                        ct.Name = category.Name;
                        ct.Discription = category.Discription;
                    }

                    User user = service.GetUserByUsersID((int)pd.SupID);

                    if (user != null)
                    {
                        model.ID = user.ID;
                        model.FirstName = user.FirstName;
                        model.LastName = user.LastName;

                    }
                
                }
                catch (Exception e) { }

                model.item = pd;
                model.categoryItem = ct;



                return View("DetailView", model);
            }


            model.Preorder = Preorder;

            return View("ShippingDetailsPage", model);
        }



        public ActionResult ConfirmPayment(ShippingModel content) 
        {
            UserModel model = new UserModel();
            ProductModel pd = new ProductModel();

            Product product = service.GetProctByProductId((int)content.ItemID);
           

            if (product != null)
            {
                pd.ID = product.ID;
                pd.Name = product.Name;
                pd.Discription = product.Discription;
                pd.StockLevel = product.StockLevel;
                pd.CtID = product.CtID;
                pd.SupID = product.SupID;
                pd.IsLive = product.IsLive;
                pd.Price = product.Price;
                pd.Size = product.Size;
                pd.ImgUrl = product.ImgUrl;

            }

            model.item = pd;

            if (content.Preorder != null && content.Preorder == false)
            {
                service.InsertDataToOrders(new webService.Order() { Total = (decimal)content.TotalPrice, OrderDate = System.DateTime.Now, OrderStatues = "Pending" });

                content.OrderID = service.GetOrderByLastOrderId().ID;

                bool stu = service.InsertDataToOrderDetails(new webService.OrderDetails() { OrderID = content.OrderID, ItemID = content.ItemID, CsName = content.CsName, CsLast = content.CsLast, AddressLine1 = content.AddressLine1, AddressLine2 = content.AddressLine2, City = content.City, Contact = content.Contact, Email = content.Email, SuppID = content.SuppID, ItemCount = content.ItemCount, OrderStatues = "Pending" });

                content.ID = service.GetLastOrderDetailsById().ID;

                int Stocks = (int)model.item.StockLevel - (int)content.ItemCount;

                bool stup = service.UpdateCurrentStockLebel(Stocks, (int)model.item.ID);

                if (stu && stup)
                {
                    model.Msgs.Add("Order Succeed");
                }

                ShippingModel sp = new ShippingModel();

                OrderDetails od = service.GetOrderDetailsById((int)content.ID);

                if (od != null)
                {

                    sp.ID = od.ID;
                    sp.OrderID = od.OrderID;
                    sp.ItemID = od.ItemID;
                    sp.CsName = od.CsName;
                    sp.CsLast = od.CsLast;
                    sp.AddressLine1 = od.AddressLine1;
                    sp.AddressLine2 = od.AddressLine2;
                    sp.City = od.City;
                    sp.Contact = od.Contact;
                    sp.Email = od.Email;
                    sp.SuppID = od.SuppID;
                    sp.ItemCount = od.ItemCount;
                    sp.OrderStatues = od.OrderStatues;

                }

                model.invoice = sp;

                model.TotalPrice = (decimal)content.TotalPrice;
                model.Preorder = false;



            }
            else 
            {
                service.InsertDataToOrders(new webService.Order() { Total = (decimal)content.TotalPrice, OrderDate = System.DateTime.Now, OrderStatues = "PreOrdered" });

                content.OrderID = service.GetOrderByLastOrderId().ID;

                bool stu = service.InsertDataToOrderDetails(new webService.OrderDetails() { OrderID = content.OrderID, ItemID = content.ItemID, CsName = content.CsName, CsLast = content.CsLast, AddressLine1 = content.AddressLine1, AddressLine2 = content.AddressLine2, City = content.City, Contact = content.Contact, Email = content.Email, SuppID = content.SuppID, ItemCount = content.ItemCount, OrderStatues = "PreOrdered" });

                content.ID = service.GetLastOrderDetailsById().ID;

                int StockLevel = (int)model.item.StockLevel - (int)content.ItemCount;

                service.InsertDataToPreOrder(new webService.PreOrder() { ItemID = (int)model.item.ID, OrderID = content.ID , SupID  = content.SuppID, Statues = "Pending", tempItemCount = content.Quantity});

                bool stup = service.UpdateCurrentStockLebel(StockLevel, (int)model.item.ID);


                if (stu)
                {
                    model.Msgs.Add("Pre order Successed");
                }

                ShippingModel sp = new ShippingModel();

                OrderDetails od = service.GetOrderDetailsById((int)content.ID);

                if (od != null) 
                {

                    sp.ID = od.ID;
                    sp.OrderID = od.OrderID;
                    sp.ItemID = od.ItemID;
                    sp.CsName = od.CsName;
                    sp.CsLast = od.CsLast;
                    sp.AddressLine1 = od.AddressLine1;
                    sp.AddressLine2 = od.AddressLine2;
                    sp.City = od.City;
                    sp.Contact = od.Contact;
                    sp.Email = od.Email;
                    sp.SuppID = od.SuppID;
                    sp.ItemCount = od.ItemCount;
                    sp.OrderStatues = od.OrderStatues;

                }

                model.invoice = sp;

                model.TotalPrice = (decimal)content.TotalPrice;
                model.Preorder = true;



            }



            //model.itemList = service.GetAllProducts().Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();



            return View("InvoiceDetailPage", model);
        }


        //public void ExportPDF(string html)
        //{


        //    var htmlToPdf = new HtmlToPdf();

        //    var PdfDoc = htmlToPdf.RenderHtmlAsPdf("hellow");

        //    var contentLength = PdfDoc.BinaryData.Length;



        //  PdfDoc.SaveAs("Invoice.pdf");

        //}



    }
}