using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.Settings
{
    public interface ISettingDefinitionManager
    {
        [NotNull]
        SettingDefinition Get([NotNull] string name);

        IReadOnlyList<SettingDefinition> GetAll();

        SettingDefinition GetOrNull(string name);
    }
}