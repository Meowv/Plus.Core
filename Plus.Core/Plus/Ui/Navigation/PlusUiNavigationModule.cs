using Plus.Authorization;
using Plus.Localization;
using Plus.Modularity;
using Plus.UI.Navigation.Localization.Resource;
using Plus.VirtualFileSystem;

namespace Plus.UI.Navigation
{
    [DependsOn(typeof(PlusUiModule), typeof(PlusAuthorizationModule))]
    public class PlusUiNavigationModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusUiNavigationModule>();
            });

            Configure<PlusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PlusUiNavigationResource>("en")
                    .AddVirtualJson("/Plus/Ui/Navigation/Localization/Resource");
            });

            Configure<PlusNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DefaultMenuContributor());
            });
        }
    }
}