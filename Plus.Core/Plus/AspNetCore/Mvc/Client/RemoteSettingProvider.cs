using Plus.DependencyInjection;
using Plus.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Client
{
    public class RemoteSettingProvider : ISettingProvider, ITransientDependency
    {
        protected ICachedApplicationConfigurationClient ConfigurationClient { get; }

        public RemoteSettingProvider(ICachedApplicationConfigurationClient configurationClient)
        {
            ConfigurationClient = configurationClient;
        }

        public async Task<string> GetOrNullAsync(string name)
        {
            var configuration = await ConfigurationClient.GetAsync();
            return configuration.Setting.Values.GetOrDefault(name);
        }

        public async Task<List<SettingValue>> GetAllAsync()
        {
            var configuration = await ConfigurationClient.GetAsync();
            return configuration
                .Setting.Values
                .Select(s => new SettingValue(s.Key, s.Value))
                .ToList();
        }
    }
}