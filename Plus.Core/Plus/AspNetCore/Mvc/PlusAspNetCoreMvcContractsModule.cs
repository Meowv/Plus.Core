using Plus.Application;
using Plus.Modularity;

namespace Plus.AspNetCore.Mvc
{
    [DependsOn(
        typeof(PlusDddApplicationModule)
        )]
    public class PlusAspNetCoreMvcContractsModule : PlusModule
    {

    }
}