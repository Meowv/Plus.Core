using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.RabbitMQ;

namespace Plus.EventBus.RabbitMq
{
    [DependsOn(
        typeof(PlusEventBusModule),
        typeof(PlusRabbitMqModule))]
    public class PlusEventBusRabbitMqModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<PlusRabbitMqEventBusOptions>(configuration.GetSection("RabbitMQ:EventBus"));
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context
                .ServiceProvider
                .GetRequiredService<RabbitMqDistributedEventBus>()
                .Initialize();
        }
    }
}