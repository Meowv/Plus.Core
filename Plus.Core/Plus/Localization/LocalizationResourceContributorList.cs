using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Localization
{
    public class LocalizationResourceContributorList : List<ILocalizationResourceContributor>
    {
        public LocalizedString GetOrNull(string cultureName, string name)
        {
            foreach (var contributor in this.AsQueryable().Reverse())
            {
                var localString = contributor.GetOrNull(cultureName, name);
                if (localString != null)
                {
                    return localString;
                }
            }

            return null;
        }

        public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
        {
            foreach (var contributor in this)
            {
                contributor.Fill(cultureName, dictionary);
            }
        }
    }
}