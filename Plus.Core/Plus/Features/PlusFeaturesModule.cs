using Microsoft.Extensions.DependencyInjection;
using Plus.Localization;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Validation;
using System;
using System.Collections.Generic;

namespace Plus.Features
{
    [DependsOn(
        typeof(PlusLocalizationAbstractionsModule),
        typeof(PlusMultiTenancyModule),
        typeof(PlusValidationModule)
        )]
    public class PlusFeaturesModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(FeatureInterceptorRegistrar.RegisterIfNeeded);
            AutoAddDefinitionProviders(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Configure<PlusFeatureOptions>(options =>
            {
                options.ValueProviders.Add<DefaultValueFeatureValueProvider>();
                options.ValueProviders.Add<EditionFeatureValueProvider>();
                options.ValueProviders.Add<TenantFeatureValueProvider>();
            });
        }

        private static void AutoAddDefinitionProviders(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IFeatureDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusFeatureOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
            });
        }
    }
}