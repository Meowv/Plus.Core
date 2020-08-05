using Plus.Guids;
using Plus.Modularity;

namespace Plus.EntityFrameworkCore.SqlServer
{
    [DependsOn(
        typeof(PlusEntityFrameworkCoreModule)
        )]
    public class PlusEntityFrameworkCoreSqlServerModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusSequentialGuidGeneratorOptions>(options =>
            {
                if (options.DefaultSequentialGuidType == null)
                {
                    options.DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
                }
            });
        }
    }
}