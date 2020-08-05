using Plus.Modularity;

namespace Plus.EntityFrameworkCore.Sqlite
{
    [DependsOn(
        typeof(PlusEntityFrameworkCoreModule)
    )]
    public class PlusEntityFrameworkCoreSqliteModule : PlusModule
    {

    }
}