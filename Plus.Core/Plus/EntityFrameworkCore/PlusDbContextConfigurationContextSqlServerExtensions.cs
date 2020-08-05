using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Plus.EntityFrameworkCore.DependencyInjection;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextConfigurationContextSqlServerExtensions
    {
        public static DbContextOptionsBuilder UseSqlServer(
            [NotNull] this PlusDbContextConfigurationContext context,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseSqlServer(context.ExistingConnection, sqlServerOptionsAction);
            }
            else
            {
                return context.DbContextOptions.UseSqlServer(context.ConnectionString, sqlServerOptionsAction);
            }
        }
    }
}