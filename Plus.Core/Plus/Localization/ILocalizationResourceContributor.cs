using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Plus.Localization
{
    public interface ILocalizationResourceContributor
    {
        void Initialize(LocalizationResourceInitializationContext context);

        LocalizedString GetOrNull(string cultureName, string name);

        void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary);
    }
}