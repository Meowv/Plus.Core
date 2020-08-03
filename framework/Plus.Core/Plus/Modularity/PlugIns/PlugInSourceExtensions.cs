using JetBrains.Annotations;
using System;
using System.Linq;

namespace Plus.Modularity.PlugIns
{
    public static class PlugInSourceExtensions
    {
        [NotNull]
        public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource
                .GetModules()
                .SelectMany(PlusModuleHelper.FindAllModuleTypes)
                .Distinct()
                .ToArray();
        }
    }
}