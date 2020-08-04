using Plus.Domain.Repositories;
using Plus.Domain.Repositories.MemoryDb;
using System;
using System.Collections.Generic;

namespace Plus.MemoryDb.DependencyInjection
{
    public class MemoryDbRepositoryRegistrar : RepositoryRegistrarBase<PlusMemoryDbContextRegistrationOptions>
    {
        public MemoryDbRepositoryRegistrar(PlusMemoryDbContextRegistrationOptions options)
            : base(options)
        {
        }

        protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
        {
            var memoryDbContext = (MemoryDbContext)Activator.CreateInstance(dbContextType);
            return memoryDbContext.GetEntityTypes();
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType)
        {
            return typeof(MemoryDbRepository<,>).MakeGenericType(dbContextType, entityType);
        }

        protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
        {
            return typeof(MemoryDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
        }
    }
}