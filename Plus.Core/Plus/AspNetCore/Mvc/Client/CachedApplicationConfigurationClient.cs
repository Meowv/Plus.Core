#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Plus.AspNetCore.Mvc.ApplicationConfigurations;
using Plus.Caching;
using Plus.DependencyInjection;
using Plus.Http.Client.DynamicProxying;
using Plus.Threading;
using Plus.Users;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Client
{
    public class CachedApplicationConfigurationClient : ICachedApplicationConfigurationClient, ITransientDependency
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected IHttpClientProxy<IPlusApplicationConfigurationAppService> Proxy { get; }
        protected ICurrentUser CurrentUser { get; }
        protected IDistributedCache<ApplicationConfigurationDto> Cache { get; }

        public CachedApplicationConfigurationClient(
            IDistributedCache<ApplicationConfigurationDto> cache,
            IHttpClientProxy<IPlusApplicationConfigurationAppService> proxy,
            ICurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor)
        {
            Proxy = proxy;
            CurrentUser = currentUser;
            HttpContextAccessor = httpContextAccessor;
            Cache = cache;
        }

        public async Task<ApplicationConfigurationDto> GetAsync()
        {
            var cacheKey = CreateCacheKey();
            var httpContext = HttpContextAccessor?.HttpContext;

            if (httpContext != null && httpContext.Items[cacheKey] is ApplicationConfigurationDto configuration)
            {
                return configuration;
            }

            configuration = await Cache.GetOrAddAsync(
                cacheKey,
                async () => await Proxy.Service.GetAsync(),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120) //TODO: Should be configurable. Default value should be higher (5 mins would be good).
                }
            );

            if (httpContext != null)
            {
                httpContext.Items[cacheKey] = configuration;
            }

            return configuration;
        }

        protected virtual string CreateCacheKey()
        {
            return $"ApplicationConfiguration_{CurrentUser.Id?.ToString("N") ?? "Anonymous"}_{CultureInfo.CurrentUICulture.Name}";
        }
    }
}

#endif