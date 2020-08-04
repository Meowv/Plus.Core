using Plus.Localization.Resources.PlusLocalization;
using Plus.Modularity;
using Plus.Settings;
using Plus.VirtualFileSystem;

namespace Plus.Localization
{
    [DependsOn(
        typeof(PlusVirtualFileSystemModule),
        typeof(PlusSettingsModule),
        typeof(PlusLocalizationAbstractionsModule)
        )]
    public class PlusLocalizationModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            PlusStringLocalizerFactory.Replace(context.Services);

            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusLocalizationModule>("Plus", "Plus");
            });

            Configure<PlusLocalizationOptions>(options =>
            {
                options
                    .Resources
                    .Add<DefaultResource>("en");

                options
                    .Resources
                    .Add<PlusLocalizationResource>("en")
                    .AddVirtualJson("/Localization/Resources/PlusLocalization");
            });
        }
    }
}