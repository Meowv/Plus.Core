using Microsoft.Extensions.DependencyInjection;
using Plus.Castle.DynamicProxy;
using Plus.Modularity;

namespace Plus.Castle
{
    public class PlusCastleCoreModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(typeof(PlusAsyncDeterminationInterceptor<>));
        }
    }
}