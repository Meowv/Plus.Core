using JetBrains.Annotations;
using Plus.Localization;

namespace Plus.Features
{
    public interface IFeatureDefinitionContext
    {
        FeatureGroupDefinition AddGroup([NotNull] string name, ILocalizableString displayName = null);

        FeatureGroupDefinition GetGroupOrNull(string name);

        void RemoveGroup(string name);
    }
}