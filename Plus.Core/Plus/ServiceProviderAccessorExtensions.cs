#if NETCOREAPP3_1

using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Plus.DependencyInjection;

namespace Plus
{
    public static class ServiceProviderAccessorExtensions
    {
        [CanBeNull]
        public static HttpContext GetHttpContext(this IServiceProviderAccessor serviceProviderAccessor)
        {
            return serviceProviderAccessor.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
        }
    }
}

#endif