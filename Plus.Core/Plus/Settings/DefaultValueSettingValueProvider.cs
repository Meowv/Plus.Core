using System.Threading.Tasks;

namespace Plus.Settings
{
    public class DefaultValueSettingValueProvider : SettingValueProvider
    {
        public const string ProviderName = "D";

        public override string Name => ProviderName;

        public DefaultValueSettingValueProvider(ISettingStore settingStore)
            : base(settingStore)
        {

        }

        public override Task<string> GetOrNullAsync(SettingDefinition setting)
        {
            return Task.FromResult(setting.DefaultValue);
        }
    }
}