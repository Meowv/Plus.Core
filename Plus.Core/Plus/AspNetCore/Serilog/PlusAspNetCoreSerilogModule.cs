#if NETCOREAPP3_1

using Plus.Modularity;
using Plus.MultiTenancy;

namespace Plus.AspNetCore.Serilog
{
    [DependsOn(
        typeof(PlusMultiTenancyModule),
        typeof(PlusAspNetCoreModule)
    )]
    public class PlusAspNetCoreSerilogModule : PlusModule
    {
    }
}

#endif