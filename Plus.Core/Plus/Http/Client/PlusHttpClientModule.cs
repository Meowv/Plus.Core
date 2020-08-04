using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Plus.Castle;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Threading;
using Plus.Validation;
using System.Linq;

namespace Plus.Http.Client
{
    [DependsOn(
        typeof(PlusHttpModule),
        typeof(PlusCastleCoreModule),
        typeof(PlusThreadingModule),
        typeof(PlusMultiTenancyModule),
        typeof(PlusValidationModule)
        )]
    public class PlusHttpClientModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<PlusRemoteServiceOptions>(configuration);
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusHttpClientOptions>(options =>
            {
                if (options.HttpClientActions.Any())
                {
                    var httpClientNames = options.HttpClientProxies.Select(x => x.Value.RemoteServiceName);
                    foreach (var httpClientName in httpClientNames)
                    {
                        foreach (var httpClientAction in options.HttpClientActions)
                        {
                            context.Services.Configure<HttpClientFactoryOptions>(httpClientName,
                                x => x.HttpClientActions.Add(httpClientAction.Invoke(httpClientName)));
                        }
                    }
                }
            });
        }
    }
}