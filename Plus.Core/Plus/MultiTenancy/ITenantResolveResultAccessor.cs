using JetBrains.Annotations;

namespace Plus.MultiTenancy
{
    public interface ITenantResolveResultAccessor
    {
        [CanBeNull]
        TenantResolveResult Result { get; set; }
    }
}