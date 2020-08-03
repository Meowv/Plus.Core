using System;
using System.Collections.Generic;
using System.Reflection;

namespace Plus.Modularity
{
    public interface IPlusModuleDescriptor
    {
        Type Type { get; }

        Assembly Assembly { get; }

        IPlusModule Instance { get; }

        bool IsLoadedAsPlugIn { get; }

        IReadOnlyList<IPlusModuleDescriptor> Dependencies { get; }
    }
}