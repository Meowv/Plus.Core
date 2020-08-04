using Plus.Localization.Resources.PlusLocalization;
using Plus.Settings;

namespace Plus.Localization
{
    public class LocalizationSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                    "en",
                    L("DisplayName:Plus.Localization.DefaultLanguage"),
                    L("Description:Plus.Localization.DefaultLanguage"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PlusLocalizationResource>(name);
        }
    }
}