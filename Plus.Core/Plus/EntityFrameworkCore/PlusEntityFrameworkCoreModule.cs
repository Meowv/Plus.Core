using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Domain;
using Plus.Modularity;
using Plus.Uow.EntityFrameworkCore;

namespace Plus.EntityFrameworkCore
{
    [DependsOn(typeof(PlusDddDomainModule))]
    public class PlusEntityFrameworkCoreModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusDbContextOptions>(options =>
            {
                options.PreConfigure(PlusDbContextConfigurationContext =>
                {
                    PlusDbContextConfigurationContext.DbContextOptions
                        .ConfigureWarnings(warnings =>
                        {
                            warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
                        });
                });
            });

            context.Services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
        }
    }
}