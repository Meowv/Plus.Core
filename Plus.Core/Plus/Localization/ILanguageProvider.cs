using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Localization
{
    public interface ILanguageProvider
    {
        Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync();
    }
}