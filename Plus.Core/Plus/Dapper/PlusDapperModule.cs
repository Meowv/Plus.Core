using Plus.Domain;
using Plus.EntityFrameworkCore;
using Plus.Modularity;

namespace Plus.Dapper
{
    [DependsOn(
        typeof(PlusDddDomainModule),
        typeof(PlusEntityFrameworkCoreModule))]
    public class PlusDapperModule : PlusModule
    {
    }
}