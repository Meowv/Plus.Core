using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Domain;
using Plus.Domain.Repositories.MongoDB;
using Plus.Modularity;
using Plus.Uow.MongoDB;

namespace Plus.MongoDB
{
    [DependsOn(typeof(PlusDddDomainModule))]
    public class PlusMongoDbModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient(
                typeof(IMongoDbContextProvider<>),
                typeof(UnitOfWorkMongoDbContextProvider<>)
            );

            context.Services.TryAddTransient(
                typeof(IMongoDbRepositoryFilterer<>),
                typeof(MongoDbRepositoryFilterer<>)
            );

            context.Services.TryAddTransient(
                typeof(IMongoDbRepositoryFilterer<,>),
                typeof(MongoDbRepositoryFilterer<,>)
            );
        }
    }
}