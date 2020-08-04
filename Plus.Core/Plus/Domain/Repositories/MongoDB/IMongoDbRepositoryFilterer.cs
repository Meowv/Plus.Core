using MongoDB.Driver;
using Plus.Domain.Entities;
using System.Collections.Generic;

namespace Plus.Domain.Repositories.MongoDB
{
    public interface IMongoDbRepositoryFilterer<TEntity> where TEntity : class, IEntity
    {
        void AddGlobalFilters(List<FilterDefinition<TEntity>> filters);
    }

    public interface IMongoDbRepositoryFilterer<TEntity, TKey> : IMongoDbRepositoryFilterer<TEntity> where TEntity : class, IEntity<TKey>
    {
        FilterDefinition<TEntity> CreateEntityFilter(TKey id, bool applyFilters = false);

        FilterDefinition<TEntity> CreateEntityFilter(TEntity entity, bool withConcurrencyStamp = false, string concurrencyStamp = null);
    }
}