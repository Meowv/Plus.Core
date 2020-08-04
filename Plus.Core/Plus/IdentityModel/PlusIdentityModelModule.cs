using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Threading;

namespace Plus.IdentityModel
{
    [DependsOn(
        typeof(PlusThreadingModule),
        typeof(PlusMultiTenancyModule)
        )]
    public class PlusIdentityModelModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddHttpClient(IdentityModelAuthenticationService.HttpClientName);

            Configure<PlusIdentityClientOptions>(configuration);
        }
    }
}