using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace  Web.Filters
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //Si la llamada es un GET no la procesamos
            if (actionContext.Request.Method == HttpMethod.Get) return;

            //Chequeamos si el modelo enviado no es null
            if (actionContext.ActionArguments.Any(x => x.Value == null))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Entity can't be empty.");
            }

            //Chequeamos si el modelo es valido (FluentValidation)
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}