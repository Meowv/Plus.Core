using System.Collections.Generic;

namespace Plus.AspNetCore.VirtualFileSystem
{
    public class PlusAspNetCoreContentOptions
    {
        public List<string> AllowedExtraWebContentFolders { get; }
        public List<string> AllowedExtraWebContentFileExtensions { get; }

        public PlusAspNetCoreContentOptions()
        {
            AllowedExtraWebContentFolders = new List<string>
            {
                "/Pages",
                "/Views",
                "/Themes"
            };

            AllowedExtraWebContentFileExtensions = new List<string>
            {
                ".js",
                ".css",
                ".png",
                ".jpg",
                ".jpeg",
                ".woff",
                ".woff2",
                ".tff",
                ".otf"
            };
        }
    }
}