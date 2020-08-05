#if NETCOREAPP3_1

using Plus.AspNetCore.Serilog;

namespace Microsoft.AspNetCore.Builder
{
    public static class PlusAspNetCoreSerilogApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePlusSerilogEnrichers(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PlusSerilogMiddleware>();
        }
    }
}

#endif