using Plus.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore
{
    public static class PlusSqliteModelBuilderExtensions
    {
        public static void UseSqlite(
            this ModelBuilder modelBuilder)
        {
            modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.Sqlite);
        }
    }
}