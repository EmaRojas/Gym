using App.Core.Infrastructure;
using App.Data.Infrastructure;
using App.Services;
using App.Services.Contract;
using App.Services.Generals;
using App.Web.Helpers;
using LightInject;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using CustomPrincipalSerializeModel = App.Web.Helpers.CustomPrincipalSerializeModel;

namespace App.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var container = new ServiceContainer();
            container.EnablePerWebRequestScope();
            container.RegisterControllers();
            container.EnableMvc();

            container.Register<IUserService, UserService>(new PerScopeLifetime());
            container.Register(typeof(IGeneralService<>), typeof(GeneralService<>),new PerScopeLifetime());

            container.Register<IUnitOfWork, UnitOfWork>(new PerScopeLifetime());
            container.Register(typeof(IRepository<>), typeof(Repository<>), new PerScopeLifetime());
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                newUser.Id = serializeModel.Id;
                newUser.FullName = serializeModel.FullName;
                newUser.Email = serializeModel.Email;
                newUser.ExpirationDate = serializeModel.ExpirationDate;
                newUser.Role = serializeModel.Role;
                newUser.CompanyName = serializeModel.CompanyName;

                HttpContext.Current.User = newUser;
            }
        }


    }
}
