using Plus.Domain.Entities;

namespace Plus.Domain.Repositories.MemoryDb
{
    public interface IMemoryDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IMemoryDatabase Database { get; }

        IMemoryDatabaseCollection<TEntity> Collection { get; }
    }

    public interface IMemoryDbRepository<TEntity, TKey> : IMemoryDbRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}