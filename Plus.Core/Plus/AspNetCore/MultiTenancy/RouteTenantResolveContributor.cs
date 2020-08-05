#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plus.MultiTenancy;
using System;

namespace Plus.AspNetCore.MultiTenancy
{
    public class RouteTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Route";

        public override string Name => ContributorName;

        protected override string GetTenantIdOrNameFromHttpContextOrNull(ITenantResolveContext context, HttpContext httpContext)
        {
            var tenantId = httpContext.GetRouteValue(context.GetPlusAspNetCoreMultiTenancyOptions().TenantKey);
            if (tenantId == null)
            {
                return null;
            }

            return Convert.ToString(tenantId);
        }
    }
}

#endif