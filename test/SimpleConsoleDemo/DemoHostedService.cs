using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plus;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleConsoleDemo
{
    public class DemoHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = PlusApplicationFactory.Create<SimpleConsoleDemoModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();

                var helloWorldService = application.ServiceProvider.GetService<IHelloWorldService>();
                helloWorldService.HelloWorld();

                application.Shutdown();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}