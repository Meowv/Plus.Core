using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.MultiTenancy
{
    public class PlusTenantResolveOptions
    {
        [NotNull]
        public List<ITenantResolveContributor> TenantResolvers { get; }

        public PlusTenantResolveOptions()
        {
            TenantResolvers = new List<ITenantResolveContributor>
            {
                new CurrentUserTenantResolveContributor()
            };
        }
    }
}