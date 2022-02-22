using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Security
{
    public class WebAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorize = false;

            var user = httpContext.User;

            var allowedRoles = string.IsNullOrEmpty(Roles) ? new string[0] : Roles.Split(',').ToArray();

            foreach (var role in allowedRoles)
            {
                if (user.IsInRole(role.Trim()))
                {
                    authorize = true;
                }
            }

            return authorize;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            var returnUrl = $"/{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{filterContext.ActionDescriptor.ActionName}/";

            var hasRole = AuthorizeCore(filterContext.HttpContext);

            var isAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;

            var isConnectionValid = ClaimsPrincipal.Current.Claims.Where(x => x.Type == "permission").Select(x => x.Value).SingleOrDefault() == ConfigurationManager.AppSettings["auth0:Permission"];

            var isAssociated = !filterContext.HttpContext.User.IsInRole("Anonimo");

            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (skipAuthorization) return;

            if (!isAuthenticated)
            {
                RedirectToLoginPage(filterContext, returnUrl);
            }
            else if (!isAssociated)
            {
                RedirectToAssociationPage(filterContext);
            }
            else if (!isConnectionValid || !hasRole)
            {
                RedirectToUnauthorizedPage(filterContext);
            }
        }

        private static void RedirectToLoginPage(AuthorizationContext filterContext, string returnUrl)
        {
            filterContext.Result = new RedirectResult("/Account/Login?returnUrl=" + returnUrl);
        }

        private static void RedirectToUnauthorizedPage(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Error/");
        }

        private static void RedirectToAssociationPage(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Associate/");
        }
    }
}