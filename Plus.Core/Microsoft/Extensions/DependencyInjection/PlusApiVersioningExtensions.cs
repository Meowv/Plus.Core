#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Plus.ApiVersioning;
using Plus.AspNetCore.Mvc;
using Plus.AspNetCore.Mvc.Conventions;
using Plus.AspNetCore.Mvc.Versioning;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusApiVersioningExtensions
    {
        public static IServiceCollection AddPlusApiVersioning(this IServiceCollection services, Action<ApiVersioningOptions> setupAction)
        {
            services.AddTransient<IRequestedApiVersion, HttpContextRequestedApiVersion>();
            services.AddTransient<IApiControllerSpecification, PlusConventionalApiControllerSpecification>();

            services.AddApiVersioning(setupAction);

            return services;
        }

        public static void ConfigurePlus(this ApiVersioningOptions options, PlusAspNetCoreMvcOptions mvcOptions)
        {
            foreach (var setting in mvcOptions.ConventionalControllers.ConventionalControllerSettings)
            {
                if (setting.ApiVersionConfigurer == null)
                {
                    ConfigureApiVersionsByConvention(options, setting);
                }
                else
                {
                    setting.ApiVersionConfigurer.Invoke(options);
                }
            }
        }

        private static void ConfigureApiVersionsByConvention(ApiVersioningOptions options, ConventionalControllerSetting setting)
        {
            foreach (var controllerType in setting.ControllerTypes)
            {
                var controllerBuilder = options.Conventions.Controller(controllerType);

                if (setting.ApiVersions.Any())
                {
                    foreach (var apiVersion in setting.ApiVersions)
                    {
                        controllerBuilder.HasApiVersion(apiVersion);
                    }
                }
                else
                {
                    if (!controllerType.IsDefined(typeof(ApiVersionAttribute), true))
                    {
                        controllerBuilder.IsApiVersionNeutral();
                    }
                }
            }
        }
    }
}

#endif