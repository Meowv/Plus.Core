using Plus.Domain.Repositories.MemoryDb;

namespace Plus.Uow.MemoryDb
{
    public class MemoryDbDatabaseApi : IDatabaseApi
    {
        public IMemoryDatabase Database { get; }

        public MemoryDbDatabaseApi(IMemoryDatabase database)
        {
            Database = database;
        }
    }
}