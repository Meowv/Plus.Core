using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextOptionsSqlServerExtensions
    {
        public static void UseSqlServer(
            [NotNull] this PlusDbContextOptions options,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }

        public static void UseSqlServer<TDbContext>(
            [NotNull] this PlusDbContextOptions options,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
            where TDbContext : PlusDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }
    }
}