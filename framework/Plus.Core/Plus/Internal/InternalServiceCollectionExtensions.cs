using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Modularity;
using Plus.Reflection;

namespace Plus.Internal
{
    internal static class InternalServiceCollectionExtensions
    {
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddLocalization();
        }

        internal static void AddCorePlusServices(this IServiceCollection services,
            IPlusApplication PlusApplication,
            PlusApplicationCreationOptions applicationCreationOptions)
        {
            var moduleLoader = new ModuleLoader();
            var assemblyFinder = new AssemblyFinder(PlusApplication);
            var typeFinder = new TypeFinder(assemblyFinder);

            if (!services.IsAdded<IConfiguration>())
            {
                services.ReplaceConfiguration(
                    ConfigurationHelper.BuildConfiguration(
                        applicationCreationOptions.Configuration
                    )
                );
            }

            services.TryAddSingleton<IModuleLoader>(moduleLoader);
            services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
            services.TryAddSingleton<ITypeFinder>(typeFinder);

            services.AddAssemblyOf<IPlusApplication>();

            services.Configure<PlusModuleLifecycleOptions>(options =>
            {
                options.Contributors.Add<OnPreApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnPostApplicationInitializationModuleLifecycleContributor>();
                options.Contributors.Add<OnApplicationShutdownModuleLifecycleContributor>();
            });
        }
    }
}