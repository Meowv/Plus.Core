using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.MongoDB;
using Plus.MongoDB.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusMongoDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TMongoDbContext>(this IServiceCollection services, Action<IPlusMongoDbContextRegistrationOptionsBuilder> optionsBuilder = null)
            where TMongoDbContext : PlusMongoDbContext
        {
            var options = new PlusMongoDbContextRegistrationOptions(typeof(TMongoDbContext), services);
            optionsBuilder?.Invoke(options);

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Transient(dbContextType, typeof(TMongoDbContext)));
            }

            new MongoDbRepositoryRegistrar(options).AddRepositories();

            return services;
        }
    }
}