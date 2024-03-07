using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices.Dbhelper;
using WebServices.Model;

namespace WebServices.BusinessLayer
{
    public class DBHandeler
    {
        DBHelper dbhelper = new DBHelper();


        public void InsertUserIntoDD(User content) 
        {
            dbhelper.InsertUser(content);
        }
        


        public User GetUserByUserEmailAddress(string email)
        {
            User user = new User();
            user = dbhelper.GetUserByEmail(email);

            return user;
        }



        public bool GetAuthentication(string email, string password)
        {
            bool user = false;

            if (email != null && password != null)
            {
                user = dbhelper.GetUserByUsernameAndPassword(email, password);
            }

            return user;
        }



        public void InsertCategoriesIntoDD(Category content)
        {
            dbhelper.InsertCategories(content);
        }



        public List<Category> GetAllCategorieData()
        {
            return dbhelper.GetAllCategories();
        }



        public Category GetCategoryByCategoryId(int id)
        {

            return dbhelper.GetCategoryByCategoryId(id); ;
        }



        public bool UpdateCategoryByCategoryId(Category cat)
        {
            dbhelper.UpdateCategoryByID(cat);

            return true;
        }


        //Insert Product Into DD
        public bool InsertProductIntoDD(Product content)
        {
            return dbhelper.InsertItems(content);
        }



        public List<Product> GetAllProductData()
        {
            return dbhelper.GetAllProdocts();
        }



        public Product GetCategoryByProductId(int id)
        {

            return dbhelper.GetProductByProductId(id); ;
        }



        public bool UpdateProductByProductId(Product item)
        {
            dbhelper.UpdateProductByID(item);

            return true;
        }



        public List<Product> GetAllProductDataByUserId(int id)
        {
            return dbhelper.GetAllProdoctsByUserID(id);
        }



        public User GetUserByUserID(int id)
        {
            User user = new User();
            user = dbhelper.GetUserByID(id);

            return user;
        }


        public bool InsertOrdersIntoDD(Order content)
        {
           return dbhelper.InsertIntoOrders(content);
        }


        public bool InsertOrderDetailsIntoDD(OrderDetails content)
        {
            return dbhelper.InsertIntoOrderDetails(content);
        }


        public bool InsertPreOrderIntoDD(PreOrder content)
        {
            return dbhelper.InsertIntoPreOrder(content);
        }



        public Order GetOrdersByOrderID(int id)
        {

            return dbhelper.GetOrderByOrderID(id); ;
        }

        public OrderDetails GetOrderDetailsByOrderDetailID(int id)
        {

            return dbhelper.GetOrderByOrderDetailsID(id); ;
        }


        public PreOrder GetPreOrdersByPreOrderID(int id)
        {

            return dbhelper.GetPreOrdersByPreOrdersID(id); ;
        }


        public Order GetOrdersByLastOrderID()
        {

            return dbhelper.GetOrderByLastOrderID(); ;
        }


        public OrderDetails GetLastOrderDetailsByOrderDetailID()
        {

            return dbhelper.GetOrderByLastOrderDetailsID();
        }



        public bool UpdateStockLevel(int stock, int id)
        {

            return dbhelper.UpdateStockByProductID(stock,id);
        }



        public List<OrderDetailsWithDateTime> GetAllOrderDetailsByOrderIdOrEmail(int? orderID, string Email)
        {
            return dbhelper.GetOrderDetailsFromOrderIDOrEmail(orderID, Email);
        }


        public List<OrderDetailsWithDateTime> GetAllOrderDetailsBySuppID(int? SuppID)
        {
            return dbhelper.GetAllOrderDetailsBySuppID(SuppID);
        }  
        
        
        
        public List<OrderDetailsWithDateTime> GetAllOrderDetails()
        {
            return dbhelper.GetAllOrderDetails();
        }



        public PreOrder GetTopPreOrderAndIsPending()
        {
            return dbhelper.GetTopPreOrderAndIsPending();
        }


        public bool UpdatePreOrdertempItemCount(int tempItemCount, int ID)
        {
            return dbhelper.UpdateTopPreOrdertempItemCount(tempItemCount, ID);
        }


        public bool UpdatePreOrderStatus(string Statues, int ID)
        {
            return dbhelper.UpdatePreOrderStatus(Statues, ID);
        }



        public bool UpdateOrderDetailsStatus(string OrderStatues, int ID)
        {
            return dbhelper.UpdateOrderDetailsStatus(OrderStatues, ID);
        }







    }
}