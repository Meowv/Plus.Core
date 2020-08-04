using Microsoft.Extensions.Localization;

namespace Plus.Localization
{
    public interface ILocalizableString
    {
        LocalizedString Localize(IStringLocalizerFactory stringLocalizerFactory);
    }
}