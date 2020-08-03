using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Modularity.PlugIns
{
    public class PlugInSourceList : List<IPlugInSource>
    {
        [NotNull]
        internal Type[] GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
                .Distinct()
                .ToArray();
        }
    }
}