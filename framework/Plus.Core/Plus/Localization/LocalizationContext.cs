using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Plus.DependencyInjection;
using System;

namespace Plus.Localization
{
    public class LocalizationContext : IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; }

        public IStringLocalizerFactory LocalizerFactory { get; }

        public LocalizationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
        }
    }
}