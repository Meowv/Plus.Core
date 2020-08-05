using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.Data;
using System;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public static class DbContextOptionsFactory
    {
        public static DbContextOptions<TDbContext> Create<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : PlusDbContext<TDbContext>
        {
            var creationContext = GetCreationContext<TDbContext>(serviceProvider);

            var context = new PlusDbContextConfigurationContext<TDbContext>(
                creationContext.ConnectionString,
                serviceProvider,
                creationContext.ConnectionStringName,
                creationContext.ExistingConnection
            );

            var options = GetDbContextOptions<TDbContext>(serviceProvider);

            PreConfigure(options, context);
            Configure(options, context);

            return context.DbContextOptions.Options;
        }

        private static void PreConfigure<TDbContext>(
            PlusDbContextOptions options,
            PlusDbContextConfigurationContext<TDbContext> context)
            where TDbContext : PlusDbContext<TDbContext>
        {
            foreach (var defaultPreConfigureAction in options.DefaultPreConfigureActions)
            {
                defaultPreConfigureAction.Invoke(context);
            }

            var preConfigureActions = options.PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (!preConfigureActions.IsNullOrEmpty())
            {
                foreach (var preConfigureAction in preConfigureActions)
                {
                    ((Action<PlusDbContextConfigurationContext<TDbContext>>)preConfigureAction).Invoke(context);
                }
            }
        }

        private static void Configure<TDbContext>(
            PlusDbContextOptions options,
            PlusDbContextConfigurationContext<TDbContext> context)
            where TDbContext : PlusDbContext<TDbContext>
        {
            var configureAction = options.ConfigureActions.GetOrDefault(typeof(TDbContext));
            if (configureAction != null)
            {
                ((Action<PlusDbContextConfigurationContext<TDbContext>>)configureAction).Invoke(context);
            }
            else if (options.DefaultConfigureAction != null)
            {
                options.DefaultConfigureAction.Invoke(context);
            }
            else
            {
                throw new PlusException(
                    $"No configuration found for {typeof(DbContext).AssemblyQualifiedName}! Use services.Configure<PlusDbContextOptions>(...) to configure it.");
            }
        }

        private static PlusDbContextOptions GetDbContextOptions<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : PlusDbContext<TDbContext>
        {
            return serviceProvider.GetRequiredService<IOptions<PlusDbContextOptions>>().Value;
        }

        private static DbContextCreationContext GetCreationContext<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : PlusDbContext<TDbContext>
        {
            var context = DbContextCreationContext.Current;
            if (context != null)
            {
                return context;
            }

            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();
            var connectionString = serviceProvider.GetRequiredService<IConnectionStringResolver>().Resolve(connectionStringName);

            return new DbContextCreationContext(
                connectionStringName,
                connectionString
            );
        }
    }
}