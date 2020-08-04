using Microsoft.Extensions.DependencyInjection;
using Plus.Data;
using Plus.Json;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Security;
using Plus.Threading;
using Plus.Timing;

namespace Plus.Auditing
{
    [DependsOn(
        typeof(PlusDataModule),
        typeof(PlusJsonModule),
        typeof(PlusTimingModule),
        typeof(PlusSecurityModule),
        typeof(PlusThreadingModule),
        typeof(PlusMultiTenancyModule)
        )]
    public class PlusAuditingModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(AuditingInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}