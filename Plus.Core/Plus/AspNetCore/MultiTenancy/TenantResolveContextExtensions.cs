using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.MultiTenancy;

namespace Plus.AspNetCore.MultiTenancy
{
    public static class TenantResolveContextExtensions
    {
        public static PlusAspNetCoreMultiTenancyOptions GetPlusAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptionsSnapshot<PlusAspNetCoreMultiTenancyOptions>>().Value;
        }
    }
}