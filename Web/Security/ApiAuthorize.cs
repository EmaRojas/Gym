using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace  Web.Security
{
    /// <summary>
    /// Clase utilizada para validar el ingreso de usuarios al sistema web api
    /// </summary>
    public class ApiAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var user = actionContext.RequestContext.Principal;

            var authorized = false;

            var allowedRoles = string.IsNullOrEmpty(Roles) ? new string[0] : Roles.Split(',').ToArray();

            foreach (var role in allowedRoles)
            {
                if (user.IsInRole(role.Trim()))
                {
                    authorized = true;
                }
            }

            return authorized;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = actionContext.RequestContext.Principal;

            if (!user.Identity.IsAuthenticated || !IsAuthorized(actionContext))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No permitido");
            }
        }
    }
}