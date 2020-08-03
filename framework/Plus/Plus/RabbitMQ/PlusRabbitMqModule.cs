using Microsoft.Extensions.DependencyInjection;
using Plus.Json;
using Plus.Modularity;
using Plus.Threading;

namespace Plus.RabbitMQ
{
    [DependsOn(
        typeof(PlusJsonModule),
        typeof(PlusThreadingModule)
        )]
    public class PlusRabbitMqModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<PlusRabbitMqOptions>(configuration.GetSection("RabbitMQ"));
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            context.ServiceProvider
                .GetRequiredService<IChannelPool>()
                .Dispose();

            context.ServiceProvider
                .GetRequiredService<IConnectionPool>()
                .Dispose();
        }
    }
}