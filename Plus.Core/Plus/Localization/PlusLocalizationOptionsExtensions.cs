using System.Collections.Generic;
using System.Linq;

namespace Plus.Localization
{
    public static class PlusLocalizationOptionsExtensions
    {
        public static PlusLocalizationOptions AddLanguagesMapOrUpdate(this PlusLocalizationOptions localizationOptions,
            string packageName, params NameValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguagesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguagesMap(this PlusLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguagesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
                : language;
        }

        public static PlusLocalizationOptions AddLanguageFilesMapOrUpdate(this PlusLocalizationOptions localizationOptions,
            string packageName, params NameValue[] maps)
        {
            foreach (var map in maps)
            {
                AddOrUpdate(localizationOptions.LanguageFilesMap, packageName, map);
            }

            return localizationOptions;
        }

        public static string GetLanguageFilesMap(this PlusLocalizationOptions localizationOptions, string packageName,
            string language)
        {
            return localizationOptions.LanguageFilesMap.TryGetValue(packageName, out var maps)
                ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
                : language;
        }

        private static void AddOrUpdate(IDictionary<string, List<NameValue>> maps, string packageName, NameValue value)
        {
            if (maps.TryGetValue(packageName, out var existMaps))
            {
                existMaps.GetOrAdd(x => x.Name == value.Name, () => value).Value = value.Value;
            }
            else
            {
                maps.Add(packageName, new List<NameValue> { value });
            }
        }
    }
}