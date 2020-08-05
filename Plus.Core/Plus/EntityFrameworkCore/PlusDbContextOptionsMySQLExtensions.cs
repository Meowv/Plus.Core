using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextOptionsMySQLExtensions
    {
        public static void UseMySQL(
                [NotNull] this PlusDbContextOptions options,
                [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseMySQL(mySQLOptionsAction);
            });
        }

        public static void UseMySQL<TDbContext>(
            [NotNull] this PlusDbContextOptions options,
            [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null)
            where TDbContext : PlusDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseMySQL(mySQLOptionsAction);
            });
        }
    }
}