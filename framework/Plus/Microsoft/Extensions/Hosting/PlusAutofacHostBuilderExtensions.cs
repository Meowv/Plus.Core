using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Plus.Autofac;

namespace Microsoft.Extensions.Hosting
{
    public static class PlusAutofacHostBuilderExtensions
    {
        public static IHostBuilder UseAutofac(this IHostBuilder hostBuilder)
        {
            var containerBuilder = new ContainerBuilder();

            return hostBuilder.ConfigureServices((_, services) =>
                {
                    services.AddObjectAccessor(containerBuilder);
                })
                .UseServiceProviderFactory(new PlusAutofacServiceProviderFactory(containerBuilder));
        }
    }
}