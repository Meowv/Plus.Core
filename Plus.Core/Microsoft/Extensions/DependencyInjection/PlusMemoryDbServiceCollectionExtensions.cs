using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.MemoryDb;
using Plus.MemoryDb.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusMemoryDbServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryDbContext<TMemoryDbContext>(this IServiceCollection services, Action<IPlusMemoryDbContextRegistrationOptionsBuilder> optionsBuilder = null)
            where TMemoryDbContext : MemoryDbContext
        {
            var options = new PlusMemoryDbContextRegistrationOptions(typeof(TMemoryDbContext), services);
            optionsBuilder?.Invoke(options);

            if (options.DefaultRepositoryDbContextType != typeof(TMemoryDbContext))
            {
                services.TryAddSingleton(options.DefaultRepositoryDbContextType, sp => sp.GetRequiredService<TMemoryDbContext>());
            }

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Singleton(dbContextType, sp => sp.GetRequiredService<TMemoryDbContext>()));
            }

            new MemoryDbRepositoryRegistrar(options).AddRepositories();

            return services;
        }
    }
}