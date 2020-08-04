using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.Settings
{
    public abstract class SettingValueProvider : ISettingValueProvider, ITransientDependency
    {
        public abstract string Name { get; }

        protected ISettingStore SettingStore { get; }

        protected SettingValueProvider(ISettingStore settingStore)
        {
            SettingStore = settingStore;
        }

        public abstract Task<string> GetOrNullAsync(SettingDefinition setting);
    }
}