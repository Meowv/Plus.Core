using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Plus.AspNetCore.TestBase;
using Plus.Autofac;
using Plus.Modularity;
using Plus.VirtualFileSystem;
using System;
using System.IO;

namespace Plus.AspNetCore
{
    [DependsOn(
        typeof(PlusAspNetCoreTestBaseModule),
        typeof(PlusAspNetCoreModule),
        typeof(PlusAutofacModule)
        )]
    public class PlusAspNetCoreTestModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<PlusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<PlusAspNetCoreTestModule>();
                //options.FileSets.ReplaceEmbeddedByPhysical<PlusAspNetCoreTestModule>(FindProjectPath(hostingEnvironment));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseVirtualFiles();
        }

        private string FindProjectPath(IWebHostEnvironment hostEnvironment)
        {
            var directory = new DirectoryInfo(hostEnvironment.ContentRootPath);

            while (directory != null && directory.Name != "Plus.AspNetCore.Tests")
            {
                directory = directory.Parent;
            }

            return directory?.FullName
                   ?? throw new Exception("Could not find the project path by beginning from " + hostEnvironment.ContentRootPath + ", going through to parents and looking for Plus.AspNetCore.Tests");
        }
    }
}