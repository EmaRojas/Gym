using System.Web.Http;
using FluentValidation.WebApi;
using  Web.Filters;

namespace  Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new UnitOfWorkActionFilter());

            config.Filters.Add(new ApiExceptionFilter());

            config.Filters.Add(new ValidateModelFilter());

            config.MapHttpAttributeRoutes();

            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}
