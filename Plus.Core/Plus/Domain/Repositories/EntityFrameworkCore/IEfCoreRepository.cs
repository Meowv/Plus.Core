using Microsoft.EntityFrameworkCore;
using Plus.Domain.Entities;

namespace Plus.Domain.Repositories.EntityFrameworkCore
{
    public interface IEfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        DbContext DbContext { get; }

        DbSet<TEntity> DbSet { get; }
    }

    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}