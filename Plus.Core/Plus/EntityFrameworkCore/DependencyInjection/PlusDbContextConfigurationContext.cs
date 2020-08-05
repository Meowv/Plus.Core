using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plus.DependencyInjection;
using System;
using System.Data.Common;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public class PlusDbContextConfigurationContext : IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; }

        public string ConnectionString { get; }

        public string ConnectionStringName { get; }

        public DbConnection ExistingConnection { get; }

        public DbContextOptionsBuilder DbContextOptions { get; protected set; }

        public PlusDbContextConfigurationContext(
            [NotNull] string connectionString,
            [NotNull] IServiceProvider serviceProvider,
            [CanBeNull] string connectionStringName,
            [CanBeNull] DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ServiceProvider = serviceProvider;
            ConnectionStringName = connectionStringName;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder()
                .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }

    public class PlusDbContextConfigurationContext<TDbContext> : PlusDbContextConfigurationContext
        where TDbContext : PlusDbContext<TDbContext>
    {
        public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

        public PlusDbContextConfigurationContext(
            string connectionString,
            [NotNull] IServiceProvider serviceProvider,
            [CanBeNull] string connectionStringName,
            [CanBeNull] DbConnection existingConnection)
            : base(
                  connectionString,
                  serviceProvider,
                  connectionStringName,
                  existingConnection)
        {
            base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>()
                .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }
}