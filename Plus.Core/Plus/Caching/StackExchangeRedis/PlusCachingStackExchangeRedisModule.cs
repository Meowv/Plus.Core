using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Modularity;
using System;

namespace Plus.Caching.StackExchangeRedis
{
    [DependsOn(
        typeof(PlusCachingModule)
        )]
    public class PlusCachingStackExchangeRedisModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddStackExchangeRedisCache(options =>
            {
                var redisConfiguration = configuration["Redis:Configuration"];
                if (!redisConfiguration.IsNullOrEmpty())
                {
                    options.Configuration = configuration["Redis:Configuration"];
                }
            });

            context.Services.Replace(ServiceDescriptor.Singleton<IDistributedCache, PlusRedisCache>());
        }
    }
}