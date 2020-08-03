using JetBrains.Annotations;
using Plus;
using Plus.Modularity;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static IPlusApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
            [NotNull] this IServiceCollection services,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : IPlusModule
        {
            return PlusApplicationFactory.Create<TStartupModule>(services, optionsAction);
        }

        public static IPlusApplicationWithExternalServiceProvider AddApplication(
            [NotNull] this IServiceCollection services,
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<PlusApplicationCreationOptions> optionsAction = null)
        {
            return PlusApplicationFactory.Create(startupModuleType, services, optionsAction);
        }
    }
}