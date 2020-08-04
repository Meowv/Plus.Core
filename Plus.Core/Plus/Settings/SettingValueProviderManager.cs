using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Settings
{
    public class SettingValueProviderManager : ISettingValueProviderManager, ISingletonDependency
    {
        public List<ISettingValueProvider> Providers => _lazyProviders.Value;
        protected PlusSettingOptions Options { get; }
        private readonly Lazy<List<ISettingValueProvider>> _lazyProviders;

        public SettingValueProviderManager(
            IServiceProvider serviceProvider,
            IOptions<PlusSettingOptions> options)
        {

            Options = options.Value;

            _lazyProviders = new Lazy<List<ISettingValueProvider>>(
                () => Options
                    .ValueProviders
                    .Select(type => serviceProvider.GetRequiredService(type) as ISettingValueProvider)
                    .ToList(),
                true
            );
        }
    }
}