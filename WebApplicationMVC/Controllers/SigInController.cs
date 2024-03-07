
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class SigInController : Controller
    {
        webService.WebService1 service = new webService.WebService1();


        // GET: SigIn
        public ActionResult Index()
        {
            UserModel model = new UserModel();

            return View("Index",model);
        }

        public ActionResult LogIn(SiginModel content)
        {
            UserModel model = new UserModel();
         
            model = GetUsernameAndPassword(content.UserName,content.Password);

            if (service.CheckUserNameAndPassword(new webService.LoginInfo() { Email = content.UserName, Password = content.Password }))
            {
                Session["userId"] = content.UserName;
                Session["userlevel"] = model.UserLevel;
                Session["usersID"] = model.ID;
                
                return RedirectToAction("index", "Admin" , new { msg = "Successfully loged in." });
            }
            else 
            {
                model.Errors.Add("Incorrect username or password");
                
            }


            return View("index",model);
        }


        public UserModel GetUsernameAndPassword(string email, string password)
        {
            UserModel model = new UserModel();

            if (service.CheckUserNameAndPassword(new webService.LoginInfo() { Email = email, Password = password }))
            {
                model.ID = service.GetUserByUsersEmail(email).ID;
                model.FirstName = service.GetUserByUsersEmail(email).FirstName;
                model.LastName = service.GetUserByUsersEmail(email).LastName;
                model.UserLevel = service.GetUserByUsersEmail(email).UserLevel;
                model.Email = service.GetUserByUsersEmail(email).Email;
            }
            else 
            {
                
                return model;
            }

            return model;
        }


    }
}