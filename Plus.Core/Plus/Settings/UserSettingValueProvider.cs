using Plus.Users;
using System.Threading.Tasks;

namespace Plus.Settings
{
    public class UserSettingValueProvider : SettingValueProvider
    {
        public const string ProviderName = "U";

        public override string Name => ProviderName;

        protected ICurrentUser CurrentUser { get; }

        public UserSettingValueProvider(ISettingStore settingStore, ICurrentUser currentUser)
            : base(settingStore)
        {
            CurrentUser = currentUser;
        }

        public override async Task<string> GetOrNullAsync(SettingDefinition setting)
        {
            if (CurrentUser.Id == null)
            {
                return null;
            }

            return await SettingStore.GetOrNullAsync(setting.Name, Name, CurrentUser.Id.ToString());
        }
    }
}