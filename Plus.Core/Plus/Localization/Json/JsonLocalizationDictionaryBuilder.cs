using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plus.Localization.Json
{
    public static class JsonLocalizationDictionaryBuilder
    {
        private static readonly JsonSerializerSettings SharedJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionaryBuilder" /> from given file.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static ILocalizationDictionary BuildFromFile(string filePath)
        {
            try
            {
                return BuildFromJsonString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new PlusException("Invalid localization file format: " + filePath, ex);
            }
        }

        /// <summary>
        ///     Builds an <see cref="JsonLocalizationDictionaryBuilder" /> from given json string.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static ILocalizationDictionary BuildFromJsonString(string jsonString)
        {
            JsonLocalizationFile jsonFile;
            try
            {
                jsonFile = JsonConvert.DeserializeObject<JsonLocalizationFile>(
                    jsonString, SharedJsonSerializerSettings);
            }
            catch (JsonException ex)
            {
                throw new PlusException("Can not parse json string. " + ex.Message);
            }

            var cultureCode = jsonFile.Culture;
            if (string.IsNullOrEmpty(cultureCode))
            {
                throw new PlusException("Culture is empty in language json file.");
            }

            var dictionary = new Dictionary<string, LocalizedString>();
            var dublicateNames = new List<string>();
            foreach (var item in jsonFile.Texts)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    throw new PlusException("The key is empty in given json string.");
                }

                if (dictionary.GetOrDefault(item.Key) != null)
                {
                    dublicateNames.Add(item.Key);
                }

                dictionary[item.Key] = new LocalizedString(item.Key, item.Value.NormalizeLineEndings());
            }

            if (dublicateNames.Count > 0)
            {
                throw new PlusException(
                    "A dictionary can not contain same key twice. There are some duplicated names: " +
                    dublicateNames.JoinAsString(", "));
            }

            return new StaticLocalizationDictionary(cultureCode, dictionary);
        }
    }
}