using Plus.DependencyInjection;
using Plus.EventBus.Distributed;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Handler : IDistributedEventHandler<TestEventData>, ITransientDependency
    {
        public async Task HandleEventAsync(TestEventData eventData)
        {
            Console.WriteLine(eventData.Value);

            await Task.CompletedTask;
        }
    }
}