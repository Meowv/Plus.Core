using JetBrains.Annotations;
using Plus.DependencyInjection;
using Plus.Domain.Entities;
using System;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public interface IPlusDbContextRegistrationOptionsBuilder : IPlusCommonDbContextRegistrationOptionsBuilder
    {
        void Entity<TEntity>([NotNull] Action<PlusEntityOptions<TEntity>> optionsAction)
            where TEntity : IEntity;
    }
}