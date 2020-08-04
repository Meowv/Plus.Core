using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.TextTemplating
{
    public interface ITemplateContentProvider
    {
        Task<string> GetContentOrNullAsync(
            [NotNull] string templateName,
            [CanBeNull] string cultureName = null,
            bool tryDefaults = true,
            bool useCurrentCultureIfCultureNameIsNull = true
        );

        Task<string> GetContentOrNullAsync(
            [NotNull] TemplateDefinition templateDefinition,
            [CanBeNull] string cultureName = null,
            bool tryDefaults = true,
            bool useCurrentCultureIfCultureNameIsNull = true
        );
    }
}