using Microsoft.Extensions.DependencyInjection;
using Plus.Localization;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Security;
using System;
using System.Collections.Generic;

namespace Plus.Settings
{
    [DependsOn(
        typeof(PlusLocalizationAbstractionsModule),
        typeof(PlusSecurityModule),
        typeof(PlusMultiTenancyModule)
        )]
    public class PlusSettingsModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusSettingOptions>(options =>
            {
                options.ValueProviders.Add<DefaultValueSettingValueProvider>();
                options.ValueProviders.Add<ConfigurationSettingValueProvider>();
                options.ValueProviders.Add<GlobalSettingValueProvider>();
                options.ValueProviders.Add<TenantSettingValueProvider>();
                options.ValueProviders.Add<UserSettingValueProvider>();
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(ISettingDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusSettingOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}