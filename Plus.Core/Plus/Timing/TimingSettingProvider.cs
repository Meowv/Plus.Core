using Plus.Localization;
using Plus.Settings;
using Plus.Timing.Localization.Resources.PlusTiming;

namespace Plus.Timing
{
    public class TimingSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(TimingSettingNames.TimeZone,
                    "UTC",
                    L("DisplayName:Plus.Timing.Timezone"),
                    L("Description:Plus.Timing.Timezone"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PlusTimingResource>(name);
        }
    }
}