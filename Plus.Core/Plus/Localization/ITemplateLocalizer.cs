using Microsoft.Extensions.Localization;

namespace Plus.Localization
{
    public interface ITemplateLocalizer
    {
        string Localize(IStringLocalizer localizer, string text);
    }
}