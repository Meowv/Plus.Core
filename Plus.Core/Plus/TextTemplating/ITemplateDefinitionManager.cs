using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.TextTemplating
{
    public interface ITemplateDefinitionManager
    {
        [NotNull]
        TemplateDefinition Get([NotNull] string name);

        [NotNull]
        IReadOnlyList<TemplateDefinition> GetAll();

        [CanBeNull]
        TemplateDefinition GetOrNull(string name);
    }
}