using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.Settings
{
    [Dependency(TryRegister = true)]
    public class NullSettingStore : ISettingStore, ISingletonDependency
    {
        public ILogger<NullSettingStore> Logger { get; set; }

        public NullSettingStore()
        {
            Logger = NullLogger<NullSettingStore>.Instance;
        }

        public Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return Task.FromResult((string)null);
        }
    }
}