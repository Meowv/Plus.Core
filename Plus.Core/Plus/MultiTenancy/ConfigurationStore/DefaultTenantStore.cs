using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plus.MultiTenancy.ConfigurationStore
{
    [Dependency(TryRegister = true)]
    public class DefaultTenantStore : ITenantStore, ITransientDependency
    {
        private readonly PlusDefaultTenantStoreOptions _options;

        public DefaultTenantStore(IOptionsSnapshot<PlusDefaultTenantStoreOptions> options)
        {
            _options = options.Value;
        }

        public Task<TenantConfiguration> FindAsync(string name)
        {
            return Task.FromResult(Find(name));
        }

        public Task<TenantConfiguration> FindAsync(Guid id)
        {
            return Task.FromResult(Find(id));
        }

        public TenantConfiguration Find(string name)
        {
            return _options.Tenants?.FirstOrDefault(t => t.Name == name);
        }

        public TenantConfiguration Find(Guid id)
        {
            return _options.Tenants?.FirstOrDefault(t => t.Id == id);
        }
    }
}