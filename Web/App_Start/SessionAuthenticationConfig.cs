using System;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof( Web.App_Start.SessionAuthenticationConfig), "PreAppStart")]

namespace  Web.App_Start
{
    public static class SessionAuthenticationConfig
    {
        public static void PreAppStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(System.IdentityModel.Services.SessionAuthenticationModule));
        }
    }
}