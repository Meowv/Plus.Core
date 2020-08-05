#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Plus.MultiTenancy;

namespace Plus.AspNetCore.MultiTenancy
{
    public class CookieTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Cookie";

        public override string Name => ContributorName;

        protected override string GetTenantIdOrNameFromHttpContextOrNull(ITenantResolveContext context, HttpContext httpContext)
        {
            return httpContext.Request?.Cookies[context.GetPlusAspNetCoreMultiTenancyOptions().TenantKey];
        }
    }
}

#endif