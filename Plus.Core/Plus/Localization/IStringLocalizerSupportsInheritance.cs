using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Plus.Localization
{
    public interface IStringLocalizerSupportsInheritance
    {
        IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, bool includeBaseLocalizers);
    }
}