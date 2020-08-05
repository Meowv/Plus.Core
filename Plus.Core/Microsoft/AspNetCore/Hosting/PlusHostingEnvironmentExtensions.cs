#if NETCOREAPP3_1

using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class PlusHostingEnvironmentExtensions
    {
        public static IConfigurationRoot BuildConfiguration(
            this IWebHostEnvironment env,
            PlusConfigurationBuilderOptions options = null)
        {
            options ??= new PlusConfigurationBuilderOptions();

            if (options.BasePath.IsNullOrEmpty())
            {
                options.BasePath = env.ContentRootPath;
            }

            if (options.EnvironmentName.IsNullOrEmpty())
            {
                options.EnvironmentName = env.EnvironmentName;
            }

            return ConfigurationHelper.BuildConfiguration(options);
        }
    }
}

#endif