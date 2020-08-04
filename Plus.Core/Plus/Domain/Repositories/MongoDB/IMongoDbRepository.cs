using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Plus.Domain.Entities;

namespace Plus.Domain.Repositories.MongoDB
{
    public interface IMongoDbRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IMongoDatabase Database { get; }

        IMongoCollection<TEntity> Collection { get; }

        IMongoQueryable<TEntity> GetMongoQueryable();
    }

    public interface IMongoDbRepository<TEntity, TKey> : IMongoDbRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}