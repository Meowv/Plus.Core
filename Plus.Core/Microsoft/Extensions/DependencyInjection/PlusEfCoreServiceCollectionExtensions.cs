using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.EntityFrameworkCore;
using Plus.EntityFrameworkCore.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusEfCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddPlusDbContext<TDbContext>(
            this IServiceCollection services,
            Action<IPlusDbContextRegistrationOptionsBuilder> optionsBuilder = null)
            where TDbContext : PlusDbContext<TDbContext>
        {
            services.AddMemoryCache();

            var options = new PlusDbContextRegistrationOptions(typeof(TDbContext), services);
            optionsBuilder?.Invoke(options);

            services.TryAddTransient(DbContextOptionsFactory.Create<TDbContext>);

            foreach (var dbContextType in options.ReplacedDbContextTypes)
            {
                services.Replace(ServiceDescriptor.Transient(dbContextType, typeof(TDbContext)));
            }

            new EfCoreRepositoryRegistrar(options).AddRepositories();

            return services;
        }
    }
}