using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System.Linq;

namespace Plus.AspNetCore.Mvc.UI.Theming
{
    public class DefaultThemeSelector : IThemeSelector, ITransientDependency
    {
        protected PlusThemingOptions Options { get; }

        public DefaultThemeSelector(IOptions<PlusThemingOptions> options)
        {
            Options = options.Value;
        }

        public virtual ThemeInfo GetCurrentThemeInfo()
        {
            if (!Options.Themes.Any())
            {
                throw new PlusException($"No theme registered! Use {nameof(PlusThemingOptions)} to register themes.");
            }

            if (Options.DefaultThemeName == null)
            {
                return Options.Themes.Values.First();
            }

            var themeInfo = Options.Themes.Values.FirstOrDefault(t => t.Name == Options.DefaultThemeName);
            if (themeInfo == null)
            {
                throw new PlusException("Default theme is configured but it's not found in the registered themes: " + Options.DefaultThemeName);
            }

            return themeInfo;
        }
    }
}