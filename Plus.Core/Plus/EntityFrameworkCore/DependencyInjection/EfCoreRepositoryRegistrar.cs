using Plus.Domain.Repositories;
using Plus.Domain.Repositories.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public class EfCoreRepositoryRegistrar : RepositoryRegistrarBase<PlusDbContextRegistrationOptions>
    {
        public EfCoreRepositoryRegistrar(PlusDbContextRegistrationOptions options)
            : base(options)
        {

        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            return DbContextHelper.GetEntityTypes(dbContextType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(EfCoreRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            return typeof(EfCoreRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
        }
    }
}