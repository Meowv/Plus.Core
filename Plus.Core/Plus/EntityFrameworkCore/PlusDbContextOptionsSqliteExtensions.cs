using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextOptionsSqliteExtensions
    {
        public static void UseSqlite(
            [NotNull] this PlusDbContextOptions options,
            [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseSqlite(sqliteOptionsAction);
            });
        }

        public static void UseSqlite<TDbContext>(
            [NotNull] this PlusDbContextOptions options,
            [CanBeNull] Action<SqliteDbContextOptionsBuilder> sqliteOptionsAction = null)
            where TDbContext : PlusDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseSqlite(sqliteOptionsAction);
            });
        }
    }
}