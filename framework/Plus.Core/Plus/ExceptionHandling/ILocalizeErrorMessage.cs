using Plus.Localization;

namespace Plus.ExceptionHandling
{
    public interface ILocalizeErrorMessage
    {
        string LocalizeMessage(LocalizationContext context);
    }
}