#if NETCOREAPP3_1

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Plus.ApiVersioning;
using Plus.AspNetCore.Mvc.ApiExploring;
using Plus.AspNetCore.Mvc.Conventions;
using Plus.AspNetCore.Mvc.DataAnnotations;
using Plus.AspNetCore.Mvc.DependencyInjection;
using Plus.AspNetCore.Mvc.Json;
using Plus.AspNetCore.Mvc.Localization;
using Plus.AspNetCore.VirtualFileSystem;
using Plus.DependencyInjection;
using Plus.DynamicProxy;
using Plus.Http;
using Plus.Http.Modeling;
using Plus.Localization;
using Plus.Modularity;
using Plus.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Plus.AspNetCore.Mvc
{
    [DependsOn(
        typeof(PlusAspNetCoreModule),
        typeof(PlusLocalizationModule),
        typeof(PlusApiVersioningAbstractionsModule),
        typeof(PlusAspNetCoreMvcContractsModule),
        typeof(PlusUiModule)
        )]
    public class PlusAspNetCoreMvcModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            DynamicProxyIgnoreTypes.Add<ControllerBase>();
            DynamicProxyIgnoreTypes.Add<PageModel>();

            context.Services.AddConventionalRegistrar(new PlusAspNetCoreMvcConventionalRegistrar());
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusApiDescriptionModelOptions>(options =>
            {
                options.IgnoredInterfaces.AddIfNotContains(typeof(IAsyncActionFilter));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IFilterMetadata));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IActionFilter));
            });

            Configure<PlusRemoteServiceApiDescriptionProviderOptions>(options =>
            {
                var statusCodes = new List<int>
                {
                    (int) HttpStatusCode.Forbidden,
                    (int) HttpStatusCode.Unauthorized,
                    (int) HttpStatusCode.BadRequest,
                    (int) HttpStatusCode.NotFound,
                    (int) HttpStatusCode.NotImplemented,
                    (int) HttpStatusCode.InternalServerError
                };

                options.SupportedResponseTypes.AddIfNotContains(statusCodes.Select(statusCode => new ApiResponseType
                {
                    Type = typeof(RemoteServiceErrorResponse),
                    StatusCode = statusCode
                }));
            });

            context.Services.PostConfigure<PlusAspNetCoreMvcOptions>(options =>
            {
                if (options.MinifyGeneratedScript == null)
                {
                    options.MinifyGeneratedScript = context.Services.GetHostingEnvironment().IsProduction();
                }
            });

            var mvcCoreBuilder = context.Services.AddMvcCore();
            context.Services.ExecutePreConfiguredActions(mvcCoreBuilder);

            var PlusMvcDataAnnotationsLocalizationOptions = context.Services
                .ExecutePreConfiguredActions(
                    new PlusMvcDataAnnotationsLocalizationOptions()
                );

            context.Services
                .AddSingleton<IOptions<PlusMvcDataAnnotationsLocalizationOptions>>(
                    new OptionsWrapper<PlusMvcDataAnnotationsLocalizationOptions>(
                        PlusMvcDataAnnotationsLocalizationOptions
                    )
                );

            var mvcBuilder = context.Services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new PlusMvcJsonContractResolver(context.Services);
                })
                .AddRazorRuntimeCompilation()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var resourceType = PlusMvcDataAnnotationsLocalizationOptions
                            .AssemblyResources
                            .GetOrDefault(type.Assembly);

                        if (resourceType != null)
                        {
                            return factory.Create(resourceType);
                        }

                        return factory.CreateDefaultOrNull() ??
                               factory.Create(type);
                    };
                })
                .AddViewLocalization(); //TODO: How to configure from the application? Also, consider to move to a UI module since APIs does not care about it.

            Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(
                    new RazorViewEngineVirtualFileProvider(
                        context.Services.GetSingletonInstance<IObjectAccessor<IServiceProvider>>()
                    )
                );
            });

            context.Services.ExecutePreConfiguredActions(mvcBuilder);

            //TODO: AddViewLocalization by default..?

            context.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Use DI to create controllers
            context.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //Use DI to create view components
            context.Services.Replace(ServiceDescriptor.Singleton<IViewComponentActivator, ServiceBasedViewComponentActivator>());

            //Use DI to create razor page
            context.Services.Replace(ServiceDescriptor.Singleton<IPageModelActivatorProvider, ServiceBasedPageModelActivatorProvider>());

            //Add feature providers
            var partManager = context.Services.GetSingletonInstance<ApplicationPartManager>();
            var application = context.Services.GetSingletonInstance<IPlusApplication>();

            partManager.FeatureProviders.Add(new PlusConventionalControllerFeatureProvider(application));
            partManager.ApplicationParts.AddIfNotContains(typeof(PlusAspNetCoreMvcModule).Assembly);

            context.Services.Replace(ServiceDescriptor.Singleton<IValidationAttributeAdapterProvider, PlusValidationAttributeAdapterProvider>());
            context.Services.AddSingleton<ValidationAttributeAdapterProvider>();

            Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddPlus(context.Services);
            });

            Configure<PlusEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(endpointContext =>
                {
                    endpointContext.Endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                    endpointContext.Endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpointContext.Endpoints.MapRazorPages();
                });
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            ApplicationPartSorter.Sort(
                context.Services.GetSingletonInstance<ApplicationPartManager>(),
                context.Services.GetSingletonInstance<IModuleContainer>()
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AddApplicationParts(context);
        }

        private static void AddApplicationParts(ApplicationInitializationContext context)
        {
            var partManager = context.ServiceProvider.GetService<ApplicationPartManager>();
            if (partManager == null)
            {
                return;
            }

            //Plugin modules
            var moduleAssemblies = context
                .ServiceProvider
                .GetRequiredService<IModuleContainer>()
                .Modules
                .Where(m => m.IsLoadedAsPlugIn)
                .Select(m => m.Type.Assembly)
                .Distinct();

            AddToApplicationParts(partManager, moduleAssemblies);

            //Controllers for application services
            var controllerAssemblies = context
                .ServiceProvider
                .GetRequiredService<IOptions<PlusAspNetCoreMvcOptions>>()
                .Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .Select(s => s.Assembly)
                .Distinct();

            AddToApplicationParts(partManager, controllerAssemblies);
        }

        private static void AddToApplicationParts(ApplicationPartManager partManager, IEnumerable<Assembly> moduleAssemblies)
        {
            foreach (var moduleAssembly in moduleAssemblies)
            {
                partManager.ApplicationParts.AddIfNotContains(moduleAssembly);
            }
        }
    }
}

#endif