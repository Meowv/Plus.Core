using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity.PlugIns;

namespace Plus
{
    public class PlusApplicationCreationOptions
    {
        [NotNull]
        public IServiceCollection Services { get; }

        [NotNull]
        public PlugInSourceList PlugInSources { get; }

        [NotNull]
        public PlusConfigurationBuilderOptions Configuration { get; }

        public PlusApplicationCreationOptions([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            PlugInSources = new PlugInSourceList();
            Configuration = new PlusConfigurationBuilderOptions();
        }
    }
}