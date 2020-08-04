using JetBrains.Annotations;
using System;

namespace Plus.Localization
{
    public interface IInheritedResourceTypesProvider
    {
        [NotNull]
        Type[] GetInheritedResourceTypes();
    }
}