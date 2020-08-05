using Plus.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore
{
    public static class PlusSqlServerModelBuilderExtensions
    {
        public static void UseSqlServer(
            this ModelBuilder modelBuilder)
        {
            modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.SqlServer);
        }
    }
}