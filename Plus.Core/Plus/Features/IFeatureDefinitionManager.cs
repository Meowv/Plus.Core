using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.Features
{
    public interface IFeatureDefinitionManager
    {
        [NotNull]
        FeatureDefinition Get([NotNull] string name);

        IReadOnlyList<FeatureDefinition> GetAll();

        FeatureDefinition GetOrNull(string name);
    }
}