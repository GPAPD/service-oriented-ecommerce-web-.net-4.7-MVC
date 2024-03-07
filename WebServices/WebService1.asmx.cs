using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebServices.BusinessLayer;
using WebServices.Model;

namespace WebServices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DBHandeler logic = new DBHandeler();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello Akash";
        }


        //Insert user to user's table
        [WebMethod]
        public bool InsertDataToUsers(string Fname, string Lname, int Ulevel, string Email,string Password)
        {
            User content = new User();

            content.FirstName = Fname;
            content.LastName = Lname;
            content.UserLevel = Ulevel;
            content.Email = Email;
            content.Password = Password;
            

            if (content != null)
            {
                logic.InsertUserIntoDD(content);
            }
            else
            {
                return false;
            }

            return true;

        }


        //Get users by Email
        [WebMethod]
        public User GetUserByUsersEmail(string Email)
        {
            User user = new User();

            if (Email != null)
            {
                user = logic.GetUserByUserEmailAddress(Email);
            }

            return user;

        }



        [WebMethod]
        public bool CheckUserNameAndPassword(LoginInfo info)
        {
            bool user = false;
            if (info.Email != null && info.Password != null)
            {
                user = logic.GetAuthentication(info.Email, info.Password);
            }

            return user;

        }

        



        //Insert Categories to Categories table
        [WebMethod]
        public bool InsertDataToCategories(string name , string dcs)
        {
            Category model = new Category();

            model.Name = name;
            model.Discription = dcs;


            if (model.Name != null && model.Discription != null)
            {
                logic.InsertCategoriesIntoDD(model);
            }
            else
            {
                return false;
            }

            return true;

        }
        


        [WebMethod]
        public List<Category> GetAllCategories()
        {
            List < Category > categories =  new List<Category>();
            categories = logic.GetAllCategorieData();


            return categories;

        }
        


        [WebMethod]
        public Category GetCategoryByCatId(int id)
        {
            Category categories = new Category();
            categories = logic.GetCategoryByCategoryId(id);

            return categories;

        }



        [WebMethod]
        public bool UpdateCategoryById (int id, string name, string description)
        {
            Category categories = new Category();
            categories.ID = id;
            categories.Name = name;
            categories.Discription = description;

            logic.UpdateCategoryByCategoryId(categories);

            return true;

        }


        [WebMethod]
        public bool InsertDataIntoProdocts(string name, string description, int stockLevel, int categoryId, int supplierId, int isLive, decimal price, int size, string imgUrl)
        {
            Product content = new Product();

            content.Name = name;
            content.Discription = description;
            content.StockLevel = stockLevel;
            content.CtID = categoryId;
            content.SupID = supplierId;
            content.IsLive = isLive;
            content.Price = price;
            content.Size = size;
            content.ImgUrl = imgUrl;

            bool stu = logic.InsertProductIntoDD(content);

            return stu;

        }


        [WebMethod]
        public List<Product> GetAllProducts()
        {
            List<Product> itemlist = new List<Product>();
            itemlist = logic.GetAllProductData(); ;


            return itemlist;

        }



        [WebMethod]
        public Product GetProctByProductId(int id)
        {
            Product item = new Product();
            item = logic.GetCategoryByProductId(id);

            return item;

        }




        [WebMethod]
        public bool UpdateProductById(int id, string name, string description, int stockLevel, int categoryId, int supplierId, decimal price, int size, string imgUrl)
        {
            Product content = new Product();

            content.ID = id;
            content.Name = name;
            content.Discription = description;
            content.StockLevel = stockLevel;
            content.CtID = categoryId;
            content.SupID = supplierId;
            content.IsLive = 1;
            content.Price = price;
            content.Size = size;
            content.ImgUrl = imgUrl;

            logic.UpdateProductByProductId(content);

            return true;

        }



        [WebMethod]
        public List<Product> GetAllProductsByUserId(int id)
        {
            List<Product> itemlist = new List<Product>();

            itemlist = logic.GetAllProductDataByUserId(id); ;


            return itemlist;

        }
        

        //Get users by ID
        [WebMethod]
        public User GetUserByUsersID(int id)
        {
            User user = new User();

            if (id != null)
            {
                user = logic.GetUserByUserID(id);
            }

            return user;

        }


        //Insert user to Ordes's table
        [WebMethod]
        public bool InsertDataToOrders(Order content)
        {
            if (content != null)
            {
                return logic.InsertOrdersIntoDD(content);
            }
            else
            {
                return false;
            }

        }


        //Insert user to Ordes's table
        [WebMethod]
        public bool InsertDataToOrderDetails(OrderDetails content)
        {
            if (content != null)
            {
                return logic.InsertOrderDetailsIntoDD(content);
            }
            else
            {
                return false;
            }

        }


        //Insert user to Ordes's table
        [WebMethod]
        public bool InsertDataToPreOrder(PreOrder content)
        {
            if (content != null)
            {
                return logic.InsertPreOrderIntoDD(content);
            }
            else
            {
                return false;
            }

        }


        //Get Orders by ID
        [WebMethod]
        public Order GetOrderByOrderId(int id)
        {

            return logic.GetOrdersByOrderID(id); ;
        }



        //Get Orders Details by ID
        [WebMethod]
        public OrderDetails GetOrderDetailsById(int id)
        {

            return logic.GetOrderDetailsByOrderDetailID(id); ;
        }



        //Get Orders Details by ID
        [WebMethod]
        public PreOrder GetPreOrderById(int id)
        {

            return logic.GetPreOrdersByPreOrderID(id); ;
        }



        //Get Orders by ID
        [WebMethod]
        public Order GetOrderByLastOrderId()
        {

            return logic.GetOrdersByLastOrderID();

        }


        //Get Orders Details by ID
        [WebMethod]
        public OrderDetails GetLastOrderDetailsById()
        {

            return logic.GetLastOrderDetailsByOrderDetailID(); 

        }


        //Get Orders Details by ID
        [WebMethod]
        public bool UpdateCurrentStockLebel(int Stock, int Id)
        {

            return logic.UpdateStockLevel(Stock,Id);

        }



        [WebMethod]
        public List<OrderDetailsWithDateTime> GetOrderDetailsFromOrderIdOrEmail(int? orderId, string Email)
        {
            List<OrderDetailsWithDateTime> list = new List<OrderDetailsWithDateTime>();
            list = logic.GetAllOrderDetailsByOrderIdOrEmail(orderId, Email); ;

            return list;

        }


        [WebMethod]
        public List<OrderDetailsWithDateTime> GetAllOrderDetailsBySuppID(int? SuppID)
        {
            List<OrderDetailsWithDateTime> list = new List<OrderDetailsWithDateTime>();
            list = logic.GetAllOrderDetailsBySuppID(SuppID); ;

            return list;

        }


        [WebMethod]
        public List<OrderDetailsWithDateTime> GetAllOrderDetails()
        {
            List<OrderDetailsWithDateTime> list = new List<OrderDetailsWithDateTime>();
            list = logic.GetAllOrderDetails(); ;

            return list;

        }


        [WebMethod]
        public PreOrder GetTopPreOrderAndIsPending()
        {
            return logic.GetTopPreOrderAndIsPending();
        }

        

        [WebMethod]
        public bool UpdateTopPreOrdertempItemCount(int tempItemCount, int ID)
        {
            return logic.UpdatePreOrdertempItemCount(tempItemCount, ID);
        }



        [WebMethod]
        public bool UpdatePreOrderStatusByID(string Statues, int ID)
        {
            return logic.UpdatePreOrderStatus(Statues, ID);
        }

        

        [WebMethod]
        public bool UpdateOrderDetailsStatusByID(string OrderStatues, int ID)
        {
            return logic.UpdateOrderDetailsStatus(OrderStatues, ID);
        }









    }
}
