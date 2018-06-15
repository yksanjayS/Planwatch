using SmartMonitoring.Filter;
using SmartMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SmartMonitoring.Controllers
{
   [InitializeSimpleMembership]
    public class UserController : Controller
    {
        /// <summary>
        /// It render the login view on request for login also call when a user session has time out.
        /// </summary>
        /// <param name="returnUrl"> It contain the route value of that action from where it call if Session is time out</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            if(ModelState.IsValid)
            {
                using (SmartMonitoringEntities dbcontext = new SmartMonitoringEntities())
                {
                    ObjectParameter objParam = new ObjectParameter("UserStatus", typeof(Boolean));
                   var loginInfo = dbcontext.CheckUserForLogin(login.LoginID,login.Password,objParam).ToList();
                    if(loginInfo.Count >0 )
                    {
                        foreach(var item in loginInfo)
                        {
                            Session["LoginID"] = Convert.ToString(item.LoginID);
                            Session["UserName"] = Convert.ToString(item.FirstName);
                            Session["UserRole"] = Convert.ToString(item.UserRole);
                            Session["UserStatus"] = Convert.ToBoolean(item.UserStatus);
                            Session["UserID"] = item.UserID;
                            WebSecurity.Login(item.LoginID, login.Password,persistCookie:login.RememberMe);
                            bool sts = WebSecurity.IsAuthenticated;
                        }
                        
                        return RedirectToAction("PlantIndex", "Plant");
                    }
                    else
                    {
                        ModelState.AddModelError("Your account has not been activate yet. Please contact sdrvice provider !", "UnAuthorizedUser");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Please enter correct loginID or Password !", "Validation");
            }
            return View(login);
        }

      
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister register)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    using (SmartMonitoringEntities dbcontext = new SmartMonitoringEntities())
                    {
                      int i = dbcontext.AddUser(register.FirstName, register.LastName, register.MiddleName, register.UserName, register.Password, register.UserRole, register.PhoneNo, register.FaxNo, register.CompanyName, register.Designation, register.Department, register.EmailID, false);
                        dbcontext.SaveChanges();
                        if(i > 0)
                        {
                            WebSecurity.CreateUserAndAccount(register.UserName, register.Password);
                            WebSecurity.Login(register.UserName, register.Password);
                        }
                       
                    }
                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
            else { }
            return View(register);
        }

        [HttpPost]
        public JsonResult UserNameExist(string UserName)
        {
            var user = Membership.GetUser(UserName);
            return Json(user == null);
        }

        #region Log Out method.    
        /// <summary>  
        /// POST: /Account/LogOff    
        /// </summary>  
        /// <returns>Return log off action</returns>  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                Session.Abandon();
                WebSecurity.Logout();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("Login", "User");
        }
        #endregion

        #region User Profile Method.    
        /// <summary>  
        /// GET: /User/UserProfile    
        /// </summary>  
        /// <returns>Return UserProfile 'View' to show current detail of current user</returns>  
        [HttpGet]
        [AllowAnonymous]
        public ActionResult UserProfile()
        {
            UserRegister user = new UserRegister();
            try
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                    var userDetail = context.getUserDetail(Convert.ToString(Session["UserID"]));
                    foreach(var item in userDetail)
                    {
                        user.FirstName = item.FirstName;
                        user.MiddleName = item.MiddleName;
                        user.LastName = item.Lastname;
                        user.PhoneNo = item.PhoneNo;
                        user.FaxNo = item.FaxNo;
                        user.CompanyName = item.Companyname;
                        user.Designation = item.Designation;
                        user.Department = item.Department;
                        user.EmailID = item.EmailID;
                        user.UserStatus = item.UserStatus;
                    }
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]

        public ActionResult updateUserProfile(UserRegister user)

        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        var userDetail = context.getUserDetail(Convert.ToString(Session["UserID"]));
                        { 
                        int i = context.updateUserProfile(Session["UserID"].ToString(), user.FirstName, user.LastName, user.MiddleName, user.UserRole, user.PhoneNo, user.FaxNo, user.CompanyName, user.Designation, user.Department, user.EmailID, user.UserStatus);
                        if (i > 0)
                        {
                            return RedirectToAction("UserProfile");
                        }
                    }
                }
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("UserProfile");
        }

        #endregion

        //
        // GET: /Home/ResetPassword
        [AllowAnonymous]
        [InitializeSimpleMembershipAttribute]
        public ActionResult ResetPassword(string code)
        {
            return View();
        }

        //
        // POST: /Home/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembershipAttribute]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                  int i = context.ResetCurrentPassword(model.UserID, model.CurrentPassword, model.NewPassword);
                    if(i > 0)
                    {
                        WebSecurity.ChangePassword(Session["UserName"].ToString(), model.CurrentPassword.ToString(), model.NewPassword.ToString());
                    }
                    else
                    {

                    }
                    return View();
                }
            }
            return View();
        }
    }
}