using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Plus.Authorization.Permissions;
using Plus.Localization;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Security;
using System;
using System.Collections.Generic;

namespace Plus.Authorization
{
    [DependsOn(
        typeof(PlusSecurityModule),
        typeof(PlusLocalizationAbstractionsModule),
        typeof(PlusMultiTenancyModule)
        )]
    public class PlusAuthorizationModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(AuthorizationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAuthorizationCore();

            context.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            Configure<PlusPermissionOptions>(options =>
            {
                options.ValueProviders.Add<UserPermissionValueProvider>();
                options.ValueProviders.Add<RolePermissionValueProvider>();
                options.ValueProviders.Add<ClientPermissionValueProvider>();
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IPermissionDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusPermissionOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}