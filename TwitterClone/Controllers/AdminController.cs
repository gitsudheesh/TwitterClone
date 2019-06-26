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
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        TwittterService obj = new TwittterService();
        // GET: Admin
        public ActionResult SignUp()
        {
            User model = new User();
            return View( model);
           
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            try
            {
                var response = obj.CreateAccount(new Person()
                {
                    User_id = model.UserName,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email,
                    joined = DateTime.Now,
                    Active = true
                });
            }
            catch(Exception ex)
            {
                HandleErrorInfo obj = new HandleErrorInfo(ex, "Admin", "SignUp");
                return RedirectToAction("SignUp");
            }
            return RedirectToAction("Dashboard", "User", new { id = model.UserName});
        }
       
        [Route("Edit/{id}")]
        public ActionResult EditProfile(string id)
        {
            User viewModel = new User();
            if (Convert.ToString(Session["UserId"]) != null)
            {
                var userDetails = obj.GetUserDetails(id);
                viewModel.UserName = userDetails.User_id;
                viewModel.FullName = userDetails.FullName;
                viewModel.Email = userDetails.Email;
                viewModel.Password = userDetails.Password;
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("Update/{id}")]
        [ActionName("Update")]
        public ActionResult UpdateProfile(User saveModel)
        {  
            if(ModelState.IsValid)
            {
                obj.UpdateProfile(new Person
                {
                    User_id = saveModel.UserName,
                    Password = saveModel.Password,
                    FullName = saveModel.FullName,
                    Email = saveModel.Email
                });
            }
            else
            {
                ModelState.AddModelError("", "Error occer while update the Profile.Check and try again.");
                return View("EditProfile", saveModel);
            }
            return RedirectToAction("Dashboard", "User", new { id = saveModel.UserName });
        }
        
        public ActionResult DeleteAccount(string userid)
        {
            try
            {
                obj.DeleteAccount(userid);
                return RedirectToAction("Index", "Login");
            }
            catch(Exception ex)
            {
                HandleErrorInfo obj = new HandleErrorInfo(ex, "Admin", "DeleteAccount");
                return RedirectToAction("EditProfile", new { id = userid });
            }
        }
    }
}