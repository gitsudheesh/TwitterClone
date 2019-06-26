using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterService;
using TwitterEntity;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    [HandleError]
    [RoutePrefix("Login")]
    public class LoginController : Controller
    {
        TwittterService obj = new TwittterService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userid,string pwd)
        {
            var result = obj.ValidatedUser(userid,pwd);

            switch(result)
            {
                case 0:
                    ViewBag.Message = "User not exist.Plesae Signup.";
                    break;
                case 1:
                    Session["UserId"] = userid;
                    return RedirectToAction("Dashboard", "User", new { id = userid });
                case 2:
                    ViewBag.Message = "Invalid UserName or Password";
                    break;
                case 3:
                    ViewBag.Message = "Your account has been Deactivated. Do you like to activate it?";
                    break;
            }
            return View("Index");
        }

        public ActionResult Signout()
        {   
            Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}