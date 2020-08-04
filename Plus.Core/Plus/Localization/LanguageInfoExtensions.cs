using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Localization
{
    public static class LanguageInfoExtensions
    {
        public static T FindByCulture<T>(
            [NotNull] this IEnumerable<T> languages,
            [NotNull] string cultureName,
            [CanBeNull] string uiCultureName = null)
        where T : class, ILanguageInfo
        {
            if (uiCultureName == null)
            {
                uiCultureName = cultureName;
            }

            var languageList = languages.ToList();

            return languageList.FirstOrDefault(l => l.CultureName == cultureName && l.UiCultureName == uiCultureName)
                   ?? languageList.FirstOrDefault(l => l.CultureName == cultureName)
                   ?? languageList.FirstOrDefault(l => l.UiCultureName == uiCultureName);
        }
    }
}