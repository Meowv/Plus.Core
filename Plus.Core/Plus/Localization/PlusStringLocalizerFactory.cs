using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Localization
{
    public class PlusStringLocalizerFactory : IStringLocalizerFactory, IPlusStringLocalizerFactoryWithDefaultResourceSupport
    {
        protected internal PlusLocalizationOptions PlusLocalizationOptions { get; }
        protected ResourceManagerStringLocalizerFactory InnerFactory { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected ConcurrentDictionary<Type, StringLocalizerCacheItem> LocalizerCache { get; }

        //TODO: It's better to use decorator pattern for IStringLocalizerFactory instead of getting ResourceManagerStringLocalizerFactory as a dependency.
        public PlusStringLocalizerFactory(
            ResourceManagerStringLocalizerFactory innerFactory,
            IOptions<PlusLocalizationOptions> plusLocalizationOptions,
            IServiceProvider serviceProvider)
        {
            InnerFactory = innerFactory;
            ServiceProvider = serviceProvider;
            PlusLocalizationOptions = plusLocalizationOptions.Value;

            LocalizerCache = new ConcurrentDictionary<Type, StringLocalizerCacheItem>();
        }

        public virtual IStringLocalizer Create(Type resourceType)
        {
            var resource = PlusLocalizationOptions.Resources.GetOrDefault(resourceType);
            if (resource == null)
            {
                return InnerFactory.Create(resourceType);
            }

            if (LocalizerCache.TryGetValue(resourceType, out var cacheItem))
            {
                return cacheItem.Localizer;
            }

            lock (LocalizerCache)
            {
                return LocalizerCache.GetOrAdd(
                    resourceType,
                    _ => CreateStringLocalizerCacheItem(resource)
                ).Localizer;
            }
        }

        private StringLocalizerCacheItem CreateStringLocalizerCacheItem(LocalizationResource resource)
        {
            foreach (var globalContributor in PlusLocalizationOptions.GlobalContributors)
            {
                resource.Contributors.Add((ILocalizationResourceContributor)Activator.CreateInstance(globalContributor));
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var context = new LocalizationResourceInitializationContext(resource, scope.ServiceProvider);

                foreach (var contributor in resource.Contributors)
                {
                    contributor.Initialize(context);
                }
            }

            return new StringLocalizerCacheItem(
                new PlusDictionaryBasedStringLocalizer(
                    resource,
                    resource.BaseResourceTypes.Select(Create).ToList()
                )
            );
        }

        public virtual IStringLocalizer Create(string baseName, string location)
        {
            //TODO: Investigate when this is called?

            return InnerFactory.Create(baseName, location);
        }

        internal static void Replace(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory, PlusStringLocalizerFactory>());
            services.AddSingleton<ResourceManagerStringLocalizerFactory>();
        }

        protected class StringLocalizerCacheItem
        {
            public PlusDictionaryBasedStringLocalizer Localizer { get; }

            public StringLocalizerCacheItem(PlusDictionaryBasedStringLocalizer localizer)
            {
                Localizer = localizer;
            }
        }

        public IStringLocalizer CreateDefaultOrNull()
        {
            if (PlusLocalizationOptions.DefaultResourceType == null)
            {
                return null;
            }

            return Create(PlusLocalizationOptions.DefaultResourceType);
        }
    }
}