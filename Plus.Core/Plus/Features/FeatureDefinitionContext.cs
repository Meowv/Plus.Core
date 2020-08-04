using Plus.Localization;
using System.Collections.Generic;

namespace Plus.Features
{
    public class FeatureDefinitionContext : IFeatureDefinitionContext
    {
        internal Dictionary<string, FeatureGroupDefinition> Groups { get; }

        public FeatureDefinitionContext()
        {
            Groups = new Dictionary<string, FeatureGroupDefinition>();
        }

        public FeatureGroupDefinition AddGroup(string name, ILocalizableString displayName = null)
        {
            Check.NotNull(name, nameof(name));

            if (Groups.ContainsKey(name))
            {
                throw new PlusException($"There is already an existing permission group with name: {name}");
            }

            return Groups[name] = new FeatureGroupDefinition(name, displayName);
        }

        public FeatureGroupDefinition GetGroupOrNull(string name)
        {
            Check.NotNull(name, nameof(name));

            if (!Groups.ContainsKey(name))
            {
                return null;
            }

            return Groups[name];
        }

        public void RemoveGroup(string name)
        {
            Check.NotNull(name, nameof(name));

            if (!Groups.ContainsKey(name))
            {
                throw new PlusException($"Undefined feature group: '{name}'.");
            }

            Groups.Remove(name);
        }
    }
}