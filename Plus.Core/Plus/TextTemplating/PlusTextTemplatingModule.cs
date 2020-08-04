using Microsoft.Extensions.DependencyInjection;
using Plus.Localization;
using Plus.Modularity;
using Plus.VirtualFileSystem;
using System;
using System.Collections.Generic;

namespace Plus.TextTemplating
{
    [DependsOn(
        typeof(PlusVirtualFileSystemModule),
        typeof(PlusLocalizationAbstractionsModule)
        )]
    public class PlusTextTemplatingModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddProvidersAndContributors(context.Services);
        }

        private static void AutoAddProvidersAndContributors(IServiceCollection services)
        {
            var definitionProviders = new List<Type>();
            var contentContributors = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(ITemplateDefinitionProvider).IsAssignableFrom(context.ImplementationType))
                {
                    definitionProviders.Add(context.ImplementationType);
                }

                if (typeof(ITemplateContentContributor).IsAssignableFrom(context.ImplementationType))
                {
                    contentContributors.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusTextTemplatingOptions>(options =>
            {
                options.DefinitionProviders.AddIfNotContains(definitionProviders);
                options.ContentContributors.AddIfNotContains(contentContributors);
            });
        }
    }
}