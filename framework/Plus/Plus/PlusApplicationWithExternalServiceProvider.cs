using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Plus
{
    internal class PlusApplicationWithExternalServiceProvider : PlusApplicationBase, IPlusApplicationWithExternalServiceProvider
    {
        public PlusApplicationWithExternalServiceProvider(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType,
                services,
                optionsAction)
        {
            services.AddSingleton<IPlusApplicationWithExternalServiceProvider>(this);
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            SetServiceProvider(serviceProvider);

            InitializeModules();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (ServiceProvider is IDisposable disposableServiceProvider)
            {
                disposableServiceProvider.Dispose();
            }
        }
    }
}