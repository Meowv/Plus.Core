#if NETCOREAPP3_1

using Microsoft.Extensions.DependencyInjection;
using Plus.AspNetCore.VirtualFileSystem;

namespace Microsoft.AspNetCore.Builder
{
    public static class VirtualFileSystemApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseVirtualFiles(this IApplicationBuilder app)
        {
            return app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = app.ApplicationServices.GetRequiredService<IWebContentFileProvider>()
                }
            );
        }
    }
}

#endif