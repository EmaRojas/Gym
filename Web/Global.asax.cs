using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightInject;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace  Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            RegisterContainer();
        }

        private static void RegisterContainer()
        {
            var container = new ServiceContainer();
            container.EnableAnnotatedPropertyInjection();
            RegisterWebApi(container);
            RegisterMvc(container);
        }

        private static void RegisterWebApi(IServiceContainer container)
        {
            container.RegisterApiControllers();
            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);
        }

        private static void RegisterMvc(IServiceContainer container)
        {
            container.RegisterControllers();
            container.EnableMvc();
        }
    }
}
