using JetBrains.Annotations;
using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.BlobStoring
{
    public class DefaultBlobProviderSelector : IBlobProviderSelector, ITransientDependency
    {
        protected IEnumerable<IBlobProvider> BlobProviders { get; }

        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        public DefaultBlobProviderSelector(
            IBlobContainerConfigurationProvider configurationProvider,
            IEnumerable<IBlobProvider> blobProviders)
        {
            ConfigurationProvider = configurationProvider;
            BlobProviders = blobProviders;
        }

        [NotNull]
        public virtual IBlobProvider Get([NotNull] string containerName)
        {
            Check.NotNull(containerName, nameof(containerName));

            var configuration = ConfigurationProvider.Get(containerName);

            if (!BlobProviders.Any())
            {
                throw new PlusException("No BLOB Storage provider was registered! At least one provider must be registered to be able to use the Blog Storing System.");
            }

            foreach (var provider in BlobProviders)
            {
                if (ProxyHelper.GetUnProxiedType(provider).IsAssignableTo(configuration.ProviderType))
                {
                    return provider;
                }
            }

            throw new PlusException(
                $"Could not find the BLOB Storage provider with the type ({configuration.ProviderType.AssemblyQualifiedName}) configured for the container {containerName} and no default provider was set."
            );
        }
    }
}