#if NETCOREAPP3_1

using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusAspNetCoreServiceCollectionExtensions
    {
        public static IWebHostEnvironment GetHostingEnvironment(this IServiceCollection services)
        {
            return services.GetSingletonInstance<IWebHostEnvironment>();
        }
    }
}

#endif