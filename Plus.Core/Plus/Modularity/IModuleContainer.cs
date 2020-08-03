using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.Modularity
{
    public interface IModuleContainer
    {
        [NotNull]
        IReadOnlyList<IPlusModuleDescriptor> Modules { get; }
    }
}