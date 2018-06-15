using SmartMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SmartMonitoring.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string baseUrl = "~/" + controllerName + "/" + actionName;
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            session["requestURL"] = baseUrl;
            if (baseUrl != "~/User/Login" && baseUrl != "~/Home/Home")
            {
                if (!session.IsNewSession)
                { }
                else
                {
                    if (session["UserName"] != null)
                    { }
                    else
                    {
                        var url = new UrlHelper(filterContext.RequestContext);
                        var loginUrl = url.Content("~/User/Login");
                        filterContext.HttpContext.Response.Redirect(loginUrl, true);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);
                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The Iadept Membership database could not be initialized. For more information, please contact to admin !", ex);
                }
            }
        }
    }
}