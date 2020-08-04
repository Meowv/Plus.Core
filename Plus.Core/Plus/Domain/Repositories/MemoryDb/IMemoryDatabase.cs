using Plus.Domain.Entities;

namespace Plus.Domain.Repositories.MemoryDb
{
    public interface IMemoryDatabase
    {
        IMemoryDatabaseCollection<TEntity> Collection<TEntity>() where TEntity : class, IEntity;

        TKey GenerateNextId<TEntity, TKey>();
    }
}