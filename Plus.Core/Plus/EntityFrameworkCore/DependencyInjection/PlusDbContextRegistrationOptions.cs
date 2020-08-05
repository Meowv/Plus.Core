using Microsoft.Extensions.DependencyInjection;
using Plus.DependencyInjection;
using Plus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public class PlusDbContextRegistrationOptions : PlusCommonDbContextRegistrationOptions, IPlusDbContextRegistrationOptionsBuilder
    {
        public Dictionary<Type, object> PlusEntityOptions { get; }

        public PlusDbContextRegistrationOptions(Type originalDbContextType, IServiceCollection services)
            : base(originalDbContextType, services)
        {
            PlusEntityOptions = new Dictionary<Type, object>();
        }

        public void Entity<TEntity>(Action<PlusEntityOptions<TEntity>> optionsAction) where TEntity : IEntity
        {
            Services.Configure<PlusEntityOptions>(options =>
            {
                options.Entity(optionsAction);
            });
        }
    }
}