using Plus.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SimpleConsoleDemo
{
    public class HelloWorldService : IHelloWorldService, ITransientDependency
    {
        public Task HelloWorld()
        {
            Console.WriteLine(nameof(HelloWorld));
            return Task.CompletedTask;
        }
    }
}