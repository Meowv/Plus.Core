using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Plus.AspNetCore.MultiTenancy;
using Plus.AspNetCore.TestBase;
using Plus.Json;
using Plus.Modularity;
using Plus.MultiTenancy;
using System.Collections.Generic;

namespace Plus.AspNetCore.App
{
    [DependsOn(
        typeof(PlusAspNetCoreMultiTenancyModule),
        typeof(PlusAspNetCoreTestBaseModule)
        )]
    public class AppModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseMultiTenancy();

            app.Run(async (ctx) =>
            {
                var currentTenant = ctx.RequestServices.GetRequiredService<ICurrentTenant>();
                var jsonSerializer = ctx.RequestServices.GetRequiredService<IJsonSerializer>();

                var dictionary = new Dictionary<string, string>
                {
                    ["TenantId"] = currentTenant.IsAvailable ? currentTenant.Id.ToString() : ""
                };

                var result = jsonSerializer.Serialize(dictionary, camelCase: false);
                await ctx.Response.WriteAsync(result);
            });
        }
    }
}