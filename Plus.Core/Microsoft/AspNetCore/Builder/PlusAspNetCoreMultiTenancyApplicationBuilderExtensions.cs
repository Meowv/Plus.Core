#if NETCOREAPP3_1

using Plus.AspNetCore.MultiTenancy;

namespace Microsoft.AspNetCore.Builder
{
    public static class PlusAspNetCoreMultiTenancyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MultiTenancyMiddleware>();
        }
    }
}

#endif