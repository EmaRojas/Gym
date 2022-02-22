using App.Core.Infrastructure;
using App.Data.Infrastructure;
using App.Services;
using App.Services.Contract;
using App.Services.Generals;
using LightInject;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
    }
}
