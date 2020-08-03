using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plus;
using Plus.Autofac;
using Plus.DependencyInjection;
using Plus.EventBus;
using Plus.EventBus.Local;
using Plus.Modularity;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var application = PlusApplicationFactory.Create<StatisicsModule>(options =>
            {
                options.Configuration.CommandLineArgs = args;
                options.UseAutofac();
            });

            Console.WriteLine("Initializing the application...");
            application.Initialize();
            Console.WriteLine("Initializing the application... OK");

            Console.WriteLine("Checking configuration...");

            var configuration = application.ServiceProvider.GetRequiredService<IConfiguration>();
            if (configuration["AppSettingKey1"] != "AppSettingValue1")
            {
                Console.WriteLine("ERROR: Could not read the configuration!");
                Console.WriteLine("Press ENTER to exit!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Checking configuration... OK");

            var writers = application.ServiceProvider.GetServices<IMessageWriter>();
            foreach (var writer in writers)
            {
                writer.Write();
            }

            var eventBus = application.ServiceProvider.GetRequiredService<ILocalEventBus>();

            eventBus.Subscribe<TestEventData>(
                async eventData =>
                {
                    Console.WriteLine(eventData.Value + 10);
                });

            Console.WriteLine("...............");

            await eventBus.PublishAsync(new TestEventData(1));
        }
    }

    [DependsOn(
        typeof(PlusAutofacModule),
        typeof(PlusEventBusModule)
    )]
    public class StatisicsModule : PlusModule
    {

    }

    public interface IMessageWriter
    {
        void Write();
    }

    public class ConsoleMessageWriter : IMessageWriter, ITransientDependency
    {
        private readonly MessageSource _messageSource;

        public ConsoleMessageWriter(MessageSource messageSource)
        {
            _messageSource = messageSource;
        }

        public void Write()
        {
            Console.WriteLine(_messageSource.GetMessage());
        }
    }

    public class MessageSource : ITransientDependency
    {
        public string GetMessage()
        {
            return "Hello Plus Framework !";
        }
    }
}