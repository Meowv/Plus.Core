using JetBrains.Annotations;
using Plus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public class PlusEntityOptions<TEntity>
        where TEntity : IEntity
    {
        public static PlusEntityOptions<TEntity> Empty { get; } = new PlusEntityOptions<TEntity>();

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> DefaultWithDetailsFunc { get; set; }
    }

    public class PlusEntityOptions
    {
        private readonly IDictionary<Type, object> _options;

        public PlusEntityOptions()
        {
            _options = new Dictionary<Type, object>();
        }

        public PlusEntityOptions<TEntity> GetOrNull<TEntity>()
            where TEntity : IEntity
        {
            return _options.GetOrDefault(typeof(TEntity)) as PlusEntityOptions<TEntity>;
        }

        public void Entity<TEntity>([NotNull] Action<PlusEntityOptions<TEntity>> optionsAction)
            where TEntity : IEntity
        {
            Check.NotNull(optionsAction, nameof(optionsAction));

            optionsAction(
                _options.GetOrAdd(
                    typeof(TEntity),
                    () => new PlusEntityOptions<TEntity>()
                ) as PlusEntityOptions<TEntity>
            );
        }
    }
}