using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Plus.EntityFrameworkCore.DependencyInjection;
using System;

namespace Plus.EntityFrameworkCore
{
    public static class PlusDbContextConfigurationContextMySQLExtensions
    {
        public static DbContextOptionsBuilder UseMySQL(
           [NotNull] this PlusDbContextConfigurationContext context,
           [CanBeNull] Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseMySql(context.ExistingConnection, mySQLOptionsAction);
            }
            else
            {
                return context.DbContextOptions.UseMySql(context.ConnectionString, mySQLOptionsAction);
            }
        }
    }
}