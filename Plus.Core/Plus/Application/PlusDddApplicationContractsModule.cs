using Plus.Application.Localization.Resources.PlusDdd;
using Plus.Auditing;
using Plus.Localization;
using Plus.Modularity;
using Plus.VirtualFileSystem;

namespace Plus.Application
{
    [DependsOn(
        typeof(PlusAuditingModule),
        typeof(PlusLocalizationModule)
        )]
    public class PlusDddApplicationContractsModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusDddApplicationContractsModule>();
            });

            Configure<PlusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PlusDddApplicationContractsResource>("en")
                    .AddVirtualJson("/Plus/Application/Localization/Resources/PlusDdd");
            });
        }
    }
}