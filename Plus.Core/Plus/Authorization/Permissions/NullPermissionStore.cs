using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Plus.DependencyInjection;
using Plus.Threading;
using System.Threading.Tasks;

namespace Plus.Authorization.Permissions
{
    public class NullPermissionStore : IPermissionStore, ISingletonDependency
    {
        public ILogger<NullPermissionStore> Logger { get; set; }

        public NullPermissionStore()
        {
            Logger = NullLogger<NullPermissionStore>.Instance;
        }

        public Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
        {
            return TaskCache.FalseResult;
        }
    }
}