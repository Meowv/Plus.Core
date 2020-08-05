using Localization.Resources.PlusUi;
using Plus.Localization;
using Plus.Modularity;
using Plus.VirtualFileSystem;

namespace Plus.UI
{
    [DependsOn(
        typeof(PlusLocalizationModule)
    )]
    public class PlusUiModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusUiModule>();
            });

            Configure<PlusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PlusUiResource>("en")
                    .AddVirtualJson("/Localization/Resources/PlusUi");
            });
        }
    }
}