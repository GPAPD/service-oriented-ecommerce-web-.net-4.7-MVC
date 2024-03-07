using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginAction(object sender, EventArgs e)
        {
           TempLogin();
        }

        public void TempLogin() 
        {
            string email = user_email.Value;
            string password = user_password.Value;

            if (email == "admin@gmail.com" && password == "admin123") 
            {
                Response.Redirect("Index.aspx");
            }

        
        }
    }
}