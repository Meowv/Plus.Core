using Plus.Guids;
using Plus.Modularity;

namespace Plus.EntityFrameworkCore.MySQL
{
    [DependsOn(
        typeof(PlusEntityFrameworkCoreModule)
        )]
    public class PlusEntityFrameworkCoreMySQLModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusSequentialGuidGeneratorOptions>(options =>
            {
                if (options.DefaultSequentialGuidType == null)
                {
                    options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
                }
            });
        }
    }
}