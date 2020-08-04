using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Domain;
using Plus.Domain.Repositories.MemoryDb;
using Plus.Modularity;
using Plus.Uow.MemoryDb;

namespace Plus.MemoryDb
{
    [DependsOn(typeof(PlusDddDomainModule))]
    public class PlusMemoryDbModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient(typeof(IMemoryDatabaseProvider<>), typeof(UnitOfWorkMemoryDatabaseProvider<>));
            context.Services.TryAddTransient(typeof(IMemoryDatabaseCollection<>), typeof(MemoryDatabaseCollection<>));
        }
    }
}