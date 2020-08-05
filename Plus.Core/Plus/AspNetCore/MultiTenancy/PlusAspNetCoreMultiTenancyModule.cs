#if NETCOREAPP3_1

using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.MultiTenancy;

namespace Plus.AspNetCore.MultiTenancy
{
    [DependsOn(
        typeof(PlusMultiTenancyModule),
        typeof(PlusAspNetCoreModule)
        )]
    public class PlusAspNetCoreMultiTenancyModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
                options.TenantResolvers.Add(new RouteTenantResolveContributor());
                options.TenantResolvers.Add(new HeaderTenantResolveContributor());
                options.TenantResolvers.Add(new CookieTenantResolveContributor());
            });
        }
    }
}

#endif