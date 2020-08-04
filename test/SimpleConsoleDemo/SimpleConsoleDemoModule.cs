using Plus.Autofac;
using Plus.Modularity;

namespace SimpleConsoleDemo
{
    [DependsOn(typeof(PlusAutofacModule))]
    public class SimpleConsoleDemoModule : PlusModule
    {

    }
}