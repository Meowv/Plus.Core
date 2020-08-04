using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.MultiTenancy;
using Plus.Threading;

namespace Plus.BlobStoring
{
    [DependsOn(
        typeof(PlusMultiTenancyModule),
        typeof(PlusThreadingModule)
        )]
    public class PlusBlobStoringModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IBlobContainer<>),
                typeof(BlobContainer<>)
            );

            context.Services.AddTransient(
                typeof(IBlobContainer),
                serviceProvider => serviceProvider
                    .GetRequiredService<IBlobContainer<DefaultContainer>>()
            );
        }
    }
}