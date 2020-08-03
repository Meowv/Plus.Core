using JetBrains.Annotations;
using System;

namespace Plus.Modularity
{
    public interface IDependedTypesProvider
    {
        [NotNull]
        Type[] GetDependedTypes();
    }
}