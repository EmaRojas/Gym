using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Helpers
{
    public class LoggedAttribute : AuthorizeAttribute
    {
        public LoggedAttribute()
        {
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (!string.IsNullOrEmpty(UserData.UserId))
            {
                authorize = true;
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Login?returnUrl=$/{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{filterContext.ActionDescriptor.ActionName}/");
        }
    }
}