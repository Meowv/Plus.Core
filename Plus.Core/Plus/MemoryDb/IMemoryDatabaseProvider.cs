using Plus.Domain.Repositories.MemoryDb;

namespace Plus.MemoryDb
{
    public interface IMemoryDatabaseProvider<TMemoryDbContext>
        where TMemoryDbContext : MemoryDbContext
    {
        TMemoryDbContext DbContext { get; }

        IMemoryDatabase GetDatabase();
    }
}