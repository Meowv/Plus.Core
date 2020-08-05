#if NETCOREAPP3_1

using Plus.Autofac;
using Plus.Http.Client;
using Plus.Modularity;

namespace Plus.AspNetCore.TestBase
{
    [DependsOn(typeof(PlusHttpClientModule))]
    [DependsOn(typeof(PlusAspNetCoreModule))]
    [DependsOn(typeof(PlusTestBaseModule))]
    [DependsOn(typeof(PlusAutofacModule))]
    public class PlusAspNetCoreTestBaseModule : PlusModule
    {

    }
}

#endif