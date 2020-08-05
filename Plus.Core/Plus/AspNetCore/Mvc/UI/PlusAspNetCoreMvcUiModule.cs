#if NETCOREAPP3_1

using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.UI.Navigation;
using Plus.VirtualFileSystem;

namespace Plus.AspNetCore.Mvc.UI
{
    [DependsOn(typeof(PlusAspNetCoreMvcModule))]
    [DependsOn(typeof(PlusUiNavigationModule))]
    public class PlusAspNetCoreMvcUiModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(PlusAspNetCoreMvcUiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusAspNetCoreMvcUiModule>();
            });
        }
    }
}

#endif