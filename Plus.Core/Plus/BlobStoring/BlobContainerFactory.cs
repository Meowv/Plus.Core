using Plus.DependencyInjection;
using Plus.MultiTenancy;
using Plus.Threading;
using System;

namespace Plus.BlobStoring
{
    public class BlobContainerFactory : IBlobContainerFactory, ITransientDependency
    {
        protected IBlobProviderSelector ProviderSelector { get; }

        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        protected ICurrentTenant CurrentTenant { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        protected IServiceProvider ServiceProvider { get; }

        public BlobContainerFactory(
            IBlobContainerConfigurationProvider configurationProvider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IBlobProviderSelector providerSelector,
            IServiceProvider serviceProvider)
        {
            ConfigurationProvider = configurationProvider;
            CurrentTenant = currentTenant;
            CancellationTokenProvider = cancellationTokenProvider;
            ProviderSelector = providerSelector;
            ServiceProvider = serviceProvider;
        }

        public virtual IBlobContainer Create(string name)
        {
            var configuration = ConfigurationProvider.Get(name);

            return new BlobContainer(
                name,
                configuration,
                ProviderSelector.Get(name),
                CurrentTenant,
                CancellationTokenProvider,
                ServiceProvider
            );
        }
    }
}