#if NETCOREAPP3_1

using Plus.AspNetCore.MultiTenancy;
using System.Collections.Generic;

namespace Plus.MultiTenancy
{
    public static class PlusMultiTenancyOptionsExtensions
    {
        public static void AddDomainTenantResolver(this PlusTenantResolveOptions options, string domainFormat)
        {
            options.TenantResolvers.InsertAfter(
                r => r is CurrentUserTenantResolveContributor,
                new DomainTenantResolveContributor(domainFormat)
            );
        }
    }
}

#endif