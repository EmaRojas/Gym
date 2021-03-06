using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Security
{
    public class AutorizarAccesoAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(UserData.ExpirationDate == String.Empty)
                return false;

            var fechaVencimiento = DateTime.ParseExact(UserData.ExpirationDate, "dd/MM/yyyy", null);

            return fechaVencimiento.Date >= DateTime.UtcNow.AddHours(-3).Date;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Suscripcion/Planes");
        }
    }

}