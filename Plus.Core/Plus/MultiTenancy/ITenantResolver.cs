using JetBrains.Annotations;

namespace Plus.MultiTenancy
{
    public interface ITenantResolver
    {
        /// <summary>
        /// Tries to resolve current tenant using registered <see cref="ITenantResolveContributor"/> implementations.
        /// </summary>
        /// <returns>
        /// Tenant id, unique name or null (if could not resolve).
        /// </returns>
        [NotNull]
        TenantResolveResult ResolveTenantIdOrName();
    }
}