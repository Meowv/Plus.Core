using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;

namespace Plus.Uow
{
    public class PlusUnitOfWorkModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}