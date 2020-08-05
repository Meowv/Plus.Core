using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Plus.EntityFrameworkCore.DependencyInjection;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextConfigurationContextSqliteExtensions
    {
        public static DbContextOptionsBuilder UseSqlite(
            [NotNull] this PlusDbContextConfigurationContext context,
            [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseSqlite(context.ExistingConnection, sqliteOptionsAction);
            }
            else
            {
                return context.DbContextOptions.UseSqlite(context.ConnectionString, sqliteOptionsAction);
            }
        }
    }
}