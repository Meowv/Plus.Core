using Plus.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore
{
    public static class PlusMySqlModelBuilderExtensions
    {
        public static void UseMySQL(
            this ModelBuilder modelBuilder)
        {
            modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.MySql);
        }
    }
}