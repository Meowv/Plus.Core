using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        protected PlusLocalizationOptions Options { get; }

        public DefaultLanguageProvider(IOptions<PlusLocalizationOptions> options)
        {
            Options = options.Value;
        }

        public Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync()
        {
            return Task.FromResult((IReadOnlyList<LanguageInfo>)Options.Languages);
        }
    }
}