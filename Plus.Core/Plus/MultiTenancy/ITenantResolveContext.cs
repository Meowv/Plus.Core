using JetBrains.Annotations;
using Plus.DependencyInjection;

namespace Plus.MultiTenancy
{
    public interface ITenantResolveContext : IServiceProviderAccessor
    {
        [CanBeNull]
        string TenantIdOrName { get; set; }

        bool Handled { get; set; }
    }
}