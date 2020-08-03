using JetBrains.Annotations;
using System;

namespace Plus.Modularity.PlugIns
{
    public interface IPlugInSource
    {
        [NotNull]
        Type[] GetModules();
    }
}