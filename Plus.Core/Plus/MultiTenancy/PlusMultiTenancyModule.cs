using Microsoft.Extensions.DependencyInjection;
using Plus.Data;
using Plus.Modularity;
using Plus.MultiTenancy.ConfigurationStore;
using Plus.Security;

namespace Plus.MultiTenancy
{
    [DependsOn(
        typeof(PlusDataModule),
        typeof(PlusSecurityModule)
        )]
    public class PlusMultiTenancyModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<PlusDefaultTenantStoreOptions>(configuration);
        }
    }
}