using Microsoft.Extensions.DependencyInjection;
using Plus.Caching;
using Plus.Http.Client;
using Plus.Localization;
using Plus.Modularity;

namespace Plus.AspNetCore.Mvc.Client
{
    [DependsOn(
        typeof(PlusHttpClientModule),
        typeof(PlusAspNetCoreMvcContractsModule),
        typeof(PlusCachingModule),
        typeof(PlusLocalizationModule)
        )]
    public class PlusAspNetCoreMvcClientModule : PlusModule
    {
        public const string RemoteServiceName = "PlusMvcClient";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(PlusAspNetCoreMvcContractsModule).Assembly,
                RemoteServiceName,
                asDefaultServices: false
            );

            Configure<PlusLocalizationOptions>(options =>
            {
                options.GlobalContributors.Add<RemoteLocalizationContributor>();
            });
        }
    }
}