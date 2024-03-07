using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC.Models;
using WebApplicationMVC.webService;

namespace WebApplicationMVC.Controllers
{
    public class AdminController : Controller
    {
        webService.WebService1 service = new webService.WebService1();

        // GET: Admin
        public ActionResult Index(string msg = null)
        {
            UserModel model = new UserModel();

            if (Session["userId"] != null)
            {
                
                if (!string.IsNullOrEmpty(msg))
                {
                    model.Msgs.Add(msg);
                }

                model.UserLevel = (int)Session["userlevel"];
                return View("index", model);
            }
            else 
            {
                return RedirectToAction("index", "Sigin");
            }
        }


        //Create New User Account
        public ActionResult CreatUserAccounts(CreateUserModel model) 
        {
            UserModel m = new UserModel();
            if (model!= null)
            {
                string fname = model.FirstName;
                string lname = model.LastName;
                string email = model.Email;
                int ulevel = (int)model.UserLevel;
                string password = model.Password;

                bool stutes = service.InsertDataToUsers(fname, lname, ulevel, email, password);
                if (stutes) 
                {
                    m.Email = (string)Session["userId"];
                    m.UserLevel = (int)Session["userlevel"];
                    m.ID = (int)Session["usersID"];

                    m.Msgs.Add("User Created");
                    return View("index", m);
                }

            }
            m.Errors.Add("Error Occuerd while inserting data");
            return View("index", m);
        }


        //Log Out
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index", "Sigin");
        }


        //Add New Item
        public ActionResult CreateNewItem(int id = 0) 
        {
            UserModel model = new UserModel();
            ProductModel pd = new ProductModel();

            if (Session["userId"] != null)
            {

                model.Email = (string)Session["userId"];
                model.UserLevel = (int)Session["userlevel"];
                model.ID = (int)Session["usersID"];

                model.category = service.GetAllCategories().Select(cat => new CategoryModel { ID = cat.ID, Name = cat.Name, Discription = cat.Discription }).ToList();

                if (model.UserLevel == 1)
                {
                    model.itemList = service.GetAllProducts().Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();
                }
                else 
                {
                    model.itemList = service.GetAllProductsByUserId((int)model.ID).Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();
                }


                if (id > 0) 
                {
                    pd.ID = service.GetProctByProductId(id).ID;
                    pd.Name = service.GetProctByProductId(id).Name;
                    pd.Discription = service.GetProctByProductId(id).Discription;
                    pd.StockLevel = service.GetProctByProductId(id).StockLevel;
                    pd.CtID = service.GetProctByProductId(id).CtID;
                    pd.SupID = service.GetProctByProductId(id).SupID;
                    pd.IsLive = service.GetProctByProductId(id).IsLive;
                    pd.Price = service.GetProctByProductId(id).Price;
                    pd.Size = service.GetProctByProductId(id).Size;
                    pd.ImgUrl = service.GetProctByProductId(id).ImgUrl;

                    pd.catName =service.GetCategoryByCatId((int)pd.CtID).Name;

                    model.item = pd;


                }

                model.item = pd;
                return View("CreateNewItem", model);
            }
            else
            {
                return RedirectToAction("index", "Sigin");
            }

        }


        //Add New Item
        public ActionResult CreateNewCategory(int id = 0)
        {
           
            UserModel model = new UserModel();
            CategoryModel cata = new CategoryModel();

            if (Session["userId"] != null)
            {
                model.Email = (string)Session["userId"];
                model.UserLevel = (int)Session["userlevel"];

                model.category = service.GetAllCategories().Select(cat => new CategoryModel { ID = cat.ID, Name = cat.Name, Discription = cat.Discription}).ToList();


                if (id > 0)
                {
                     cata.ID = service.GetCategoryByCatId(id).ID;
                     cata.Name = service.GetCategoryByCatId(id).Name;
                     cata.Discription = service.GetCategoryByCatId(id).Discription;

                    model.categoryItem = cata;
                }
                model.categoryItem = cata;

                return View("CreateNewCategory", model);
            }
            else
            {
                return RedirectToAction("index", "Sigin");
            }

        }


        public ActionResult SaveCategory(CategoryModel content)
        {
            UserModel model = new UserModel();

            if (content != null) 
            {
                if (content.ID > 0)
                {

                    int id = (int)content.ID;
                    string name = content.Name;
                    string description = content.Discription;

                    bool stu = service.UpdateCategoryById(id, name, description);
                    if (stu)
                    {
                        model.Msgs.Add("Category Updated");
                    }
                    else 
                    {
                        model.Errors.Add("Fail To Updated Category");
                    }
                }
                else 
                {
                    string name = content.Name;
                    string discription = content.Discription;

                    bool stu = service.InsertDataToCategories(name, discription);

                    if (stu)
                    {
                        model.Msgs.Add("Category Created");
                    }
                    else
                    {
                        model.Errors.Add("Fail to Create Category");
                    }
                }

                model.Email = (string)Session["userId"];
                model.UserLevel = (int)Session["userlevel"]; ;

            }
            CategoryModel cata = new CategoryModel();
            model.categoryItem = cata;
            model.category = service.GetAllCategories().Select(cat => new CategoryModel { ID = cat.ID, Name = cat.Name, Discription = cat.Discription }).ToList();

            return View ("CreateNewCategory", model);
        }

        public ActionResult SaveProduct(ProductModel content)
        {
            UserModel model = new UserModel();

            if (content != null )
            {
                if (content.ID > 0)
                {

                    int id = (int)content.ID;
                    string name = content.Name;
                    string description = content.Discription;
                    int stockLevel = (int)content.StockLevel;
                    int categoryId = (int)content.CtID;
                    int supplierId = (int)content.SupID;
                    decimal price = (decimal)content.Price;
                    int size = (int)content.Size;
                    string imgUrl = content.ImgUrl;

                    bool stu = service.UpdateProductById( id,  name,  description,  stockLevel,  categoryId,  supplierId,  price,  size,  imgUrl);
                    if (stu)
                    {
                        model.Msgs.Add("Item Updated");
                    }
                    else
                    {
                        model.Errors.Add("Fail To Updated Category");
                    }
                }
                else
                {
                    string name = content.Name;
                    string description = content.Discription;
                    int stockLevel = (int)content.StockLevel;
                    int categoryId = (int)content.CtID;
                    int supplierId = (int)content.SupID;
                    int isLive = 1;
                    decimal price = (decimal)content.Price;
                    int size = (int)content.Size;
                    string imgUrl = content.ImgUrl;

                    bool stu = service.InsertDataIntoProdocts(name, description, stockLevel, categoryId, supplierId, isLive, price, size, imgUrl);

                    if (stu)
                    {
                        model.Msgs.Add("Category Created");
                    }
                    else
                    {
                        model.Errors.Add("Fail to Create Category");
                    }
                }



            }

            UserModel md = new UserModel();
            CategoryModel ct = new CategoryModel();
            ProductModel pd = new ProductModel();
            md.Email = (string)Session["userId"];
            md.UserLevel = (int)Session["userlevel"];
            md.ID = (int)Session["usersID"];

            md.item = pd;
            itemListTable(md);

            return View("CreateNewItem", md);

        }




        public ActionResult UpdateItemStock(int id = 0)
        {
            UserModel model = new UserModel();

            CategoryModel ct = new CategoryModel();
            ProductModel pd = new ProductModel();

            model.Email = (string)Session["userId"];
            model.UserLevel = (int)Session["userlevel"];
            model.ID = (int)Session["usersID"];
            if (id > 0)
            {
                getItemByItemId(id, model, pd, ct);
            }
            else 
            {
                model.item = pd;

                model.categoryItem = ct;
            }

            itemListTable(model);

            return View("UpdateItemDetails", model);
        }


        

        public ActionResult SaveItemStock(int ID = 0 , int? stock = null, int? currentStock = null) 
        {

            UserModel model = new UserModel();
            ProductModel pd = new ProductModel();

            model.Email = (string)Session["userId"];
            model.UserLevel = (int)Session["userlevel"];
            model.ID = (int)Session["usersID"];

            if (ID <= 0)
            {
                model.Errors.Add("Pls select the item");
                itemListTable(model);
                model.item = pd;
                return View("UpdateItemDetails", model);
            }

            if (stock == null || stock <= 0)
            {
                model.Errors.Add("Pls add a valide stock level");
                itemListTable(model);
                model.item = pd;
                return View("UpdateItemDetails", model);
            }

            else if (stock != null && ID > 0 && currentStock != null)
            {
                int AvailabeleStock = (int)currentStock + (int)stock;

                service.UpdateCurrentStockLebel(AvailabeleStock, ID);

                while (stock != 0) {

                    PreOrder order = new PreOrder();
                    order.ID = 0; 
                    order = service.GetTopPreOrderAndIsPending();

                     if (order != null)
                    {
                        int newStock;

                        int tempICount = (int)order.tempItemCount;

                        if (stock < tempICount)
                        {
                            newStock = tempICount - (int)stock;
                            service.UpdateTopPreOrdertempItemCount(newStock, (int)order.ID);
                            newStock = 0;
                        }
                        else if (stock == tempICount)
                        {
                            service.UpdateTopPreOrdertempItemCount(0, (int)order.ID);
                            service.UpdateOrderDetailsStatusByID("Pending", (int)order.OrderID);
                            bool stu = service.UpdatePreOrderStatusByID("Done", (int)order.ID);
                            newStock = 0;
                        }
                        else
                        {
                            newStock = (int)stock - tempICount;
                            service.UpdateTopPreOrdertempItemCount(0, (int)order.ID);
                            service.UpdateOrderDetailsStatusByID("Pending", (int)order.OrderID);
                            bool stu = service.UpdatePreOrderStatusByID("Done", (int)order.ID);

                            if (stu)
                            {
                                model.Msgs.Add("PreOrder " + (int)order.ID + " Is Sent");
                            }
                        }


                        if (newStock <= 0)
                        {
                            stock = 0;
                        }
                        else
                        {
                            stock = newStock;
                        }

                    }
                    else 
                    {
                        stock = 0;
                    }
                    
                }

                model.Msgs.Add("Stock Updated");
            }

            model.item = pd;
            itemListTable(model);

            return View("UpdateItemDetails", model);
        }


        //Item List table
        public void itemListTable(UserModel model) 
        {
            CategoryModel ct = new CategoryModel();
           

            if (model.UserLevel == 1)
            {
                model.itemList = service.GetAllProducts().Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();
            }
            else
            {
                model.itemList = service.GetAllProductsByUserId((int)model.ID).Select(item => new ProductModel { ID = item.ID, Name = item.Name, Discription = item.Discription, StockLevel = item.StockLevel, CtID = item.CtID, SupID = item.SupID, IsLive = item.IsLive, Price = item.Price, Size = item.Size, ImgUrl = item.ImgUrl }).ToList();
            }

            model.category = service.GetAllCategories().Select(cat => new CategoryModel { ID = cat.ID, Name = cat.Name, Discription = cat.Discription }).ToList();
        }




        public void getItemByItemId(int id ,UserModel model, ProductModel pd , CategoryModel ct) 
        {
            if (id > 0)
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

                model.item = pd;

                model.categoryItem = ct;
            }

        }




        public ActionResult CusOrders(int id)
        {
            UserModel model = new UserModel();

            List<OrderDetailsModel> od = new List<OrderDetailsModel>();

            model.OrderList = od;

            if (id == 1)
            {
                model.OrderList = service.GetAllOrderDetails().Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
            }
            else 
            {
                model.OrderList = service.GetAllOrderDetailsBySuppID(id).Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
            }

            return View("CusOrders", model);
        }




        //public ActionResult SearchMyOrders(int OrderId = 0, string Email = null)
        //{

        //    UserModel model = new UserModel();

        //    List<OrderDetailsModel> ord = new List<OrderDetailsModel>();

        //    model.OrderList = ord;


        //    if (OrderId > 0)
        //    {
        //        model.OrderList = service.GetOrderDetailsFromOrderIdOrEmail(OrderId, Email).Select(od => new OrderDetailsModel { ID = od.ID, OrderID = od.OrderID, ItemID = od.ItemID, CsName = od.CsName, CsLast = od.CsLast, AddressLine1 = od.AddressLine1, AddressLine2 = od.AddressLine2, City = od.City, Contact = od.Contact, Email = od.Email, SuppID = od.SuppID, ItemCount = od.ItemCount, OrderStatues = od.OrderStatues, OrderDate = od.OrderDate, Total = od.Total }).ToList();
        //    }
        //    else
        //    {
        //        model.OrderList = service.GetOrderDetailsFromOrderIdOrEmail(null, Email).Select(od => new OrderDetailsModel { ID = od.ID, OrderID = od.OrderID, ItemID = od.ItemID, CsName = od.CsName, CsLast = od.CsLast, AddressLine1 = od.AddressLine1, AddressLine2 = od.AddressLine2, City = od.City, Contact = od.Contact, Email = od.Email, SuppID = od.SuppID, ItemCount = od.ItemCount, OrderStatues = od.OrderStatues, OrderDate = od.OrderDate, Total = od.Total }).ToList();
        //    }

        //    return View("MyOrders", model);
        //}





        public ActionResult UpdateOrderStatus(int id = 0)
        {
            UserModel model = new UserModel();
            List<OrderDetailsModel> od = new List<OrderDetailsModel>();

            if (id > 0) 
            {
                string statues = service.GetOrderDetailsById(id).OrderStatues;
                if (statues == "PreOrdered") 
                {
                    model.Errors.Add("Can not change the order status in a PreOrderd Item Pls Update the stock");

                    model.ID = (int)Session["usersID"];

                    if (model.ID == 1)
                    {
                        model.OrderList = service.GetAllOrderDetails().Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
                    }
                    else
                    {
                        model.OrderList = service.GetAllOrderDetailsBySuppID(model.ID).Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
                    }

                    return View("CusOrders", model);


                }
            }

            bool stu = service.UpdateOrderDetailsStatusByID("Shipped", id);
            if (stu)
            {
                model.Msgs.Add("Status Updated");
            }
            else 
            {
                model.Errors.Add("Cannot update the status");
            }
            

            model.ID = (int)Session["usersID"];

            if (model.ID == 1)
            {
                model.OrderList = service.GetAllOrderDetails().Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
            }
            else
            {
                model.OrderList = service.GetAllOrderDetailsBySuppID(model.ID).Select(ord => new OrderDetailsModel { ID = ord.ID, OrderID = ord.OrderID, ItemID = ord.ItemID, CsName = ord.CsName, CsLast = ord.CsLast, AddressLine1 = ord.AddressLine1, AddressLine2 = ord.AddressLine2, City = ord.City, Contact = ord.Contact, Email = ord.Email, SuppID = ord.SuppID, ItemCount = ord.ItemCount, OrderStatues = ord.OrderStatues, OrderDate = ord.OrderDate, Total = ord.Total }).ToList();
            }

            return View("CusOrders", model);
        }






    }
}