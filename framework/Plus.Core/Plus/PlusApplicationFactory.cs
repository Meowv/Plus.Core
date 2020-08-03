using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using System;

namespace Plus
{
    public static class PlusApplicationFactory
    {
        public static IPlusApplicationWithInternalServiceProvider Create<TStartupModule>(
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IPlusModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        public static IPlusApplicationWithInternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
        {
            return new PlusApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
        }

        public static IPlusApplicationWithExternalServiceProvider Create<TStartupModule>(
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IPlusModule
        {
            return Create(typeof(TStartupModule), services, optionsAction);
        }

        public static IPlusApplicationWithExternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
        {
            return new PlusApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
        }
    }
}