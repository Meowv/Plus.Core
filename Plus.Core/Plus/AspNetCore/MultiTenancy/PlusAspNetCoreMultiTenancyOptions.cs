using Plus.MultiTenancy;

namespace Plus.AspNetCore.MultiTenancy
{
    public class PlusAspNetCoreMultiTenancyOptions
    {
        /// <summary>
        /// Default: <see cref="TenantResolverConsts.DefaultTenantKey"/>.
        /// </summary>
        public string TenantKey { get; set; }

        public PlusAspNetCoreMultiTenancyOptions()
        {
            TenantKey = TenantResolverConsts.DefaultTenantKey;
        }
    }
}