using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServices.Model;

namespace WebServices.Dbhelper
{
    public class DBHelper
    {
        //SQL Connection
        private SqlConnection connectToDatabase()
        {
            string connectionString;
            SqlConnection con;

            //DESKTOP - TK59QET\MSSQLSERVER02
            connectionString = @"Data Source=DESKTOP-TK59QET\MSSQLSERVER02;Initial Catalog=DD;Integrated Security=True";

            con = new SqlConnection(connectionString);

            return con;
        }



        /*Add New User*/
        public bool InsertUser(User content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertUser", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@FirstName", content.FirstName);
                newCommand.Parameters.AddWithValue("@LastName", content.LastName);
                newCommand.Parameters.AddWithValue("@UserLevel", content.UserLevel);
                newCommand.Parameters.AddWithValue("@Email", content.Email);
                newCommand.Parameters.AddWithValue("@Password", content.Password);
                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }



        /* Get User Records by Email */
        public User GetUserByEmail(string email)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserByEmail", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.ID = (int)reader["ID"];
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.UserLevel = (int)reader["UserLevel"];
                    user.Email = reader["Email"].ToString();
                    user.Password = reader["Password"].ToString();

                    return user;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }



        public bool GetUserByUsernameAndPassword(string username, string password)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUsersByEmailAndPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", username);
                command.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = command.ExecuteReader();
                bool userFound = reader.Read();

                return userFound;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        /*Add new Category*/
        public bool InsertCategories(Category content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertCategories", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@Name", content.Name);
                newCommand.Parameters.AddWithValue("@Discription", content.Discription);
          
                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }

        /* Get All Categories Records */
        public List<Category> GetAllCategories()
        {
            SqlConnection connection = connectToDatabase();
            List<Category> list = new List<Category>();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllCategories", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                int count = 0;



                    while (reader.Read())
                    {
                        Category cat = new Category();

                        cat.ID = (int)reader["ID"];
                        cat.Name = reader["Name"].ToString();
                        cat.Discription = reader["Discription"].ToString();

                        count++;

                        list.Add(cat);
                    }


            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return list;
        }



        /* Get User Records by Email */
        public Category GetCategoryByCategoryId (int ID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetCategoryByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Category cat = new Category();
                    cat.ID = (int)reader["ID"];
                    cat.Name = reader["Name"].ToString();
                    cat.Discription = reader["Discription"].ToString();

                    return cat;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }



        /* Update Category Records by ID */
        public bool UpdateCategoryByID(Category content)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateCategoryByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", content.ID);
                command.Parameters.AddWithValue("@Name", content.Name);
                command.Parameters.AddWithValue("@Discription", content.Discription);

                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        /*Add New User*/
        public bool InsertItems(Product content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertProducts", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@Name", content.Name);
                newCommand.Parameters.AddWithValue("@Discription", content.Discription);
                newCommand.Parameters.AddWithValue("@StockLevel", content.StockLevel);
                newCommand.Parameters.AddWithValue("@CtID", content.CtID);
                newCommand.Parameters.AddWithValue("@SupID", content.SupID);
                newCommand.Parameters.AddWithValue("@IsLive", content.IsLive);
                newCommand.Parameters.AddWithValue("@Price", content.Price);
                newCommand.Parameters.AddWithValue("@Size", content.Size);
                newCommand.Parameters.AddWithValue("@ImgUrl", content.ImgUrl);
                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }



        /* Get All Prodocts Records */
        public List<Product> GetAllProdocts()
        {
            SqlConnection connection = connectToDatabase();
            List<Product> list = new List<Product>();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetAllProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                int count = 0;



                while (reader.Read())
                {
                    Product item = new Product();

                    item.ID = (int)reader["ID"];
                    item.Name = reader["Name"].ToString();
                    item.Discription = reader["Discription"].ToString();
                    item.StockLevel = (int)reader["StockLevel"];
                    item.CtID = (int)reader["CtID"];
                    item.SupID = (int)reader["SupID"];
                    item.IsLive = (byte)reader["IsLive"];
                    item.Price = (decimal)reader["Price"];
                    item.Size = (int)reader["Size"];
                    item.ImgUrl = reader["ImgUrl"].ToString();
                    count++;

                    list.Add(item);
                }


            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return list;
        }



        /* Get Product Data by Product ID */
        public Product GetProductByProductId(int ID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetProductByProductID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Product item = new Product();
                    item.ID = (int)reader["ID"];
                    item.Name = reader["Name"].ToString();
                    item.Discription = reader["Discription"].ToString();
                    item.StockLevel = (int)reader["StockLevel"];
                    item.CtID = (int)reader["CtID"];
                    item.SupID = (int)reader["SupID"];
                    item.IsLive = (byte)reader["IsLive"];
                    item.Price = (decimal)reader["Price"];
                    item.Size = (int)reader["Size"];
                    item.ImgUrl = reader["ImgUrl"].ToString();

                    return item;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }



        /* Update Category Records by Product ID */
        public bool UpdateProductByID(Product content)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand newCommand = new SqlCommand("UpdateProductDataByID", connection);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@ID", content.ID);
                newCommand.Parameters.AddWithValue("@Name", content.Name);
                newCommand.Parameters.AddWithValue("@Discription", content.Discription);
                newCommand.Parameters.AddWithValue("@StockLevel", content.StockLevel);
                newCommand.Parameters.AddWithValue("@CtID", content.CtID);
                newCommand.Parameters.AddWithValue("@SupID", content.SupID);
                newCommand.Parameters.AddWithValue("@IsLive", content.IsLive);
                newCommand.Parameters.AddWithValue("@Price", content.Price);
                newCommand.Parameters.AddWithValue("@Size", content.Size);
                newCommand.Parameters.AddWithValue("@ImgUrl", content.ImgUrl);

                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }




        /* Get All Prodocts Records By Users id */
        public List<Product> GetAllProdoctsByUserID(int id)
        {
            SqlConnection connection = connectToDatabase();
            List<Product> list = new List<Product>();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetProductByUserID", connection);
                command.Parameters.AddWithValue("@SupID", id);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                int count = 0;



                while (reader.Read())
                {
                    Product item = new Product();

                    item.ID = (int)reader["ID"];
                    item.Name = reader["Name"].ToString();
                    item.Discription = reader["Discription"].ToString();
                    item.StockLevel = (int)reader["StockLevel"];
                    item.CtID = (int)reader["CtID"];
                    item.SupID = (int)reader["SupID"];
                    item.IsLive = (byte)reader["IsLive"];
                    item.Price = (decimal)reader["Price"];
                    item.Size = (int)reader["Size"];
                    item.ImgUrl = reader["ImgUrl"].ToString();
                    count++;

                    list.Add(item);
                }


            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return list;
        }



        /* Get User Records by Email */
        public User GetUserByID(int id)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User();
                    user.ID = (int)reader["ID"];
                    user.FirstName = reader["FirstName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.UserLevel = (int)reader["UserLevel"];
                    user.Email = reader["Email"].ToString();
                    user.Password = reader["Password"].ToString();

                    return user;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }




        /*Add New Orders*/
        public bool InsertIntoOrders(Order content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertIntoOrders", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@Total", content.Total);
                newCommand.Parameters.AddWithValue("@OrderDate", content.OrderDate);
                newCommand.Parameters.AddWithValue("@OrderStatues", content.OrderStatues);
                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }




        /*Add New OrderDetails*/
        public bool InsertIntoOrderDetails(OrderDetails content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertIntoOrderDetails", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@OrderID", content.OrderID);
                newCommand.Parameters.AddWithValue("@ItemID", content.ItemID);
                newCommand.Parameters.AddWithValue("@CsName", content.CsName);
                newCommand.Parameters.AddWithValue("@CsLast", content.CsLast);
                newCommand.Parameters.AddWithValue("@AddressLine1", content.AddressLine1);
                newCommand.Parameters.AddWithValue("@AddressLine2", content.AddressLine2);
                newCommand.Parameters.AddWithValue("@City", content.City);
                newCommand.Parameters.AddWithValue("@Contact", content.Contact);
                newCommand.Parameters.AddWithValue("@Email", content.Email);
                newCommand.Parameters.AddWithValue("@SuppID", content.SuppID);
                newCommand.Parameters.AddWithValue("@ItemCount", content.ItemCount);
                newCommand.Parameters.AddWithValue("@OrderStatues", content.OrderStatues);

                newCommand.ExecuteNonQuery();

                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }


        

        /*Add New PreOrder*/
        public bool InsertIntoPreOrder(PreOrder content)
        {
            SqlConnection newCon = connectToDatabase();

            try
            {
                newCon.Open();
                SqlCommand newCommand = new SqlCommand("InsertIntoPreOrder", newCon);
                newCommand.CommandType = CommandType.StoredProcedure;
                newCommand.Parameters.AddWithValue("@ItemID", content.ItemID);
                newCommand.Parameters.AddWithValue("@OrderID", content.OrderID);
                newCommand.Parameters.AddWithValue("@SupID", content.SupID);
                newCommand.Parameters.AddWithValue("@Statues", content.Statues);
                newCommand.Parameters.AddWithValue("@tempItemCount", content.tempItemCount);
                newCommand.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (newCon != null)
                {
                    newCon.Close();
                }

            }
        }




        /* Get Order Records by Order ID */
        public Order GetOrderByOrderID(int id)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetOrderByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Order or = new Order();
                    or.ID = (int)reader["ID"];
                    or.Total = (int)reader["Total"];
                    or.OrderDate = (DateTime)reader["OrderDate"];
                    or.OrderStatues = reader["OrderStatues"].ToString();

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }



        /* Get OrderDetails Records by OrderDetails id */
        public OrderDetails GetOrderByOrderDetailsID(int id)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetOrderDetailsByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    OrderDetails or = new OrderDetails();
                    or.ID = (int)reader["ID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.CsName = reader["CsName"].ToString();
                    or.CsLast = reader["CsLast"].ToString();
                    or.AddressLine1 = reader["AddressLine1"].ToString();
                    or.AddressLine2 = reader["AddressLine2"].ToString();
                    or.City = reader["City"].ToString();
                    or.Contact = reader["Contact"].ToString();
                    or.Email = reader["Email"].ToString();
                    or.SuppID = (int)reader["SuppID"];
                    or.ItemCount = (int)reader["ItemCount"];
                    or.OrderStatues = reader["OrderStatues"].ToString();

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        /* Get Order Records by Order ID */
        public PreOrder GetPreOrdersByPreOrdersID(int id)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetPreOrdersByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PreOrder or = new PreOrder();
                    or.ID = (int)reader["ID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.SupID = (int)reader["SupID"];
                    or.Statues = reader["Statues"].ToString();

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        /* Get Order Records by Order ID */
        public Order GetOrderByLastOrderID()
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetLastOrderID", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Order or = new Order();
                    or.ID = (int)reader["ID"];
                    or.Total = (decimal)reader["Total"];
                    or.OrderDate = (DateTime)reader["OrderDate"];
                    or.OrderStatues = reader["OrderStatues"].ToString();

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        /* Get OrderDetails Records by OrderDetails id */
        public OrderDetails GetOrderByLastOrderDetailsID()
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetLastOrderDetailsID", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    OrderDetails or = new OrderDetails();
                    or.ID = (int)reader["ID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.CsName = reader["CsName"].ToString();
                    or.CsLast = reader["CsLast"].ToString();
                    or.AddressLine1 = reader["AddressLine1"].ToString();
                    or.AddressLine2 = reader["AddressLine2"].ToString();
                    or.City = reader["City"].ToString();
                    or.Contact = reader["Contact"].ToString();
                    or.Email = reader["Email"].ToString();
                    or.SuppID = (int)reader["SuppID"];
                    or.ItemCount = (int)reader["ItemCount"];
                    or.OrderStatues = reader["OrderStatues"].ToString();

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        /* Update Category Records by ID */
        public bool UpdateStockByProductID(int stock, int id)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateSockLevel", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@StockLevel", stock);

                command.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        public List<OrderDetailsWithDateTime> GetOrderDetailsFromOrderIDOrEmail(int? OrderID , string Email )
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                List<OrderDetailsWithDateTime> list = new List <OrderDetailsWithDateTime>();

                connection.Open();
                SqlCommand command = new SqlCommand("GetOrderDetailsFromOrderIDOrEmail", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (OrderID == null)
                {
                    command.Parameters.AddWithValue("@OrderID", DBNull.Value);
                }
                else 
                {
                    command.Parameters.AddWithValue("@OrderID", OrderID);
                }
                command.Parameters.AddWithValue("@Email", Email);

                SqlDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    OrderDetailsWithDateTime or = new OrderDetailsWithDateTime();
                    or.ID = (int)reader["ID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.CsName = reader["CsName"].ToString();
                    or.CsLast = reader["CsLast"].ToString();
                    or.AddressLine1 = reader["AddressLine1"].ToString();
                    or.AddressLine2 = reader["AddressLine2"].ToString();
                    or.City = reader["City"].ToString();
                    or.Contact = reader["Contact"].ToString();
                    or.Email = reader["Email"].ToString();
                    or.SuppID = (int)reader["SuppID"];
                    or.ItemCount = (int)reader["ItemCount"];
                    or.OrderStatues = reader["OrderStatues"].ToString();
                    or.Total = (decimal)reader["Total"];
                    or.OrderDate = (DateTime)reader["OrderDate"];

                    count++;

                    list.Add(or);
                }

                return list; 
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }






        public List<OrderDetailsWithDateTime> GetAllOrderDetailsBySuppID(int? SuppID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                List<OrderDetailsWithDateTime> list = new List<OrderDetailsWithDateTime>();

                connection.Open();
                SqlCommand command = new SqlCommand("GetAllOrderDetailsBySuppID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SuppID", SuppID);


                SqlDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    OrderDetailsWithDateTime or = new OrderDetailsWithDateTime();
                    or.ID = (int)reader["ID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.CsName = reader["CsName"].ToString();
                    or.CsLast = reader["CsLast"].ToString();
                    or.AddressLine1 = reader["AddressLine1"].ToString();
                    or.AddressLine2 = reader["AddressLine2"].ToString();
                    or.City = reader["City"].ToString();
                    or.Contact = reader["Contact"].ToString();
                    or.Email = reader["Email"].ToString();
                    or.SuppID = (int)reader["SuppID"];
                    or.ItemCount = (int)reader["ItemCount"];
                    or.OrderStatues = reader["OrderStatues"].ToString();
                    or.Total = (decimal)reader["Total"];
                    or.OrderDate = (DateTime)reader["OrderDate"];

                    count++;

                    list.Add(or);
                }

                return list;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        public List<OrderDetailsWithDateTime> GetAllOrderDetails()
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                List<OrderDetailsWithDateTime> list = new List<OrderDetailsWithDateTime>();

                connection.Open();
                SqlCommand command = new SqlCommand("GetAllOrderDetails", connection);
                command.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    OrderDetailsWithDateTime or = new OrderDetailsWithDateTime();
                    or.ID = (int)reader["ID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.CsName = reader["CsName"].ToString();
                    or.CsLast = reader["CsLast"].ToString();
                    or.AddressLine1 = reader["AddressLine1"].ToString();
                    or.AddressLine2 = reader["AddressLine2"].ToString();
                    or.City = reader["City"].ToString();
                    or.Contact = reader["Contact"].ToString();
                    or.Email = reader["Email"].ToString();
                    or.SuppID = (int)reader["SuppID"];
                    or.ItemCount = (int)reader["ItemCount"];
                    or.OrderStatues = reader["OrderStatues"].ToString();
                    or.Total = (decimal)reader["Total"];
                    or.OrderDate = (DateTime)reader["OrderDate"];

                    count++;

                    list.Add(or);
                }

                return list;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }


        public PreOrder GetTopPreOrderAndIsPending()
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetTopPreOrderAndIsPending", connection);
                command.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PreOrder or = new PreOrder();
                    or.ID = (int)reader["ID"];
                    or.ItemID = (int)reader["ItemID"];
                    or.OrderID = (int)reader["OrderID"];
                    or.SupID = (int)reader["SupID"];
                    or.Statues = reader["Statues"].ToString();
                    or.tempItemCount = (int)reader["tempItemCount"];

                    return or;
                }

                return null; // No user found with the specified email
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }



        public bool UpdateTopPreOrdertempItemCount(int tempItemCount, int ID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateTopPreOrdertempItemCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@tempItemCount", tempItemCount);
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
                return true;

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }




        public bool UpdatePreOrderStatus(string Statues, int ID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdatePreOrderStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Statues", Statues);
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
                return true;

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }




        public bool UpdateOrderDetailsStatus(string OrderStatues, int ID)
        {
            SqlConnection connection = connectToDatabase();

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UpdateOrderDetailsStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderStatues", OrderStatues);
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
                return true;

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }












    }
}