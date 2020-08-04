using Microsoft.Extensions.DependencyInjection;
using Plus.Localization;
using Plus.Modularity;
using Plus.Validation.Localization;
using Plus.VirtualFileSystem;
using System;
using System.Collections.Generic;

namespace Plus.Validation
{
    [DependsOn(
        typeof(PlusValidationAbstractionsModule),
        typeof(PlusLocalizationModule)
        )]
    public class PlusValidationModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(ValidationInterceptorRegistrar.RegisterIfNeeded);
            AutoAddObjectValidationContributors(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusValidationResource>();
            });

            Configure<PlusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PlusValidationResource>("en")
                    .AddVirtualJson("/Volo/Plus/Validation/Localization");
            });
        }

        private static void AutoAddObjectValidationContributors(IServiceCollection services)
        {
            var contributorTypes = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IObjectValidationContributor).IsAssignableFrom(context.ImplementationType))
                {
                    contributorTypes.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusValidationOptions>(options =>
            {
                options.ObjectValidationContributors.AddIfNotContains(contributorTypes);
            });
        }
    }
}