using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity.PlugIns;
using System;

namespace Plus.Modularity
{
    public interface IModuleLoader
    {
        [NotNull]
        IPlusModuleDescriptor[] LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType,
            [NotNull] PlugInSourceList plugInSources
        );
    }
}
