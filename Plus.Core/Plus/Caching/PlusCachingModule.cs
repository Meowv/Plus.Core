using Microsoft.Extensions.DependencyInjection;
using Plus.Json;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Serialization;
using Plus.Threading;
using System;

namespace Plus.Caching
{
    [DependsOn(
        typeof(PlusThreadingModule),
        typeof(PlusSerializationModule),
        typeof(PlusMultiTenancyModule),
        typeof(PlusJsonModule))]
    public class PlusCachingModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMemoryCache();
            context.Services.AddDistributedMemoryCache();

            context.Services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            context.Services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

            context.Services.Configure<PlusDistributedCacheOptions>(cacheOptions =>
            {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            });
        }
    }
}