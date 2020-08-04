using Plus.Auditing;
using Plus.Data;
using Plus.EventBus;
using Plus.Guids;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.ObjectMapping;
using Plus.Threading;
using Plus.Timing;
using Plus.Uow;

namespace Plus.Domain
{
    [DependsOn(
        typeof(PlusAuditingModule),
        typeof(PlusDataModule),
        typeof(PlusEventBusModule),
        typeof(PlusGuidsModule),
        typeof(PlusMultiTenancyModule),
        typeof(PlusThreadingModule),
        typeof(PlusTimingModule),
        typeof(PlusUnitOfWorkModule),
        typeof(PlusObjectMappingModule)
        )]
    public class PlusDddDomainModule : PlusModule
    {

    }
}