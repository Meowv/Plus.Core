#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Plus.MultiTenancy;
using Plus.Text.Formatting;
using System;

namespace Plus.AspNetCore.MultiTenancy
{
    //TODO: Create a better domain format. We can accept regex for example.

    public class DomainTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Domain";

        public override string Name => ContributorName;

        private static readonly string[] ProtocolPrefixes = { "http://", "https://" };

        private readonly string _domainFormat;

        public DomainTenantResolveContributor(string domainFormat)
        {
            _domainFormat = domainFormat.RemovePreFix(ProtocolPrefixes);
        }

        protected override string GetTenantIdOrNameFromHttpContextOrNull(
            ITenantResolveContext context,
            HttpContext httpContext)
        {
            if (httpContext.Request?.Host == null)
            {
                return null;
            }

            var hostName = httpContext.Request.Host.Host.RemovePreFix(ProtocolPrefixes);
            var extractResult = FormattedStringValueExtracter.Extract(hostName, _domainFormat, ignoreCase: true);

            context.Handled = true;

            if (!extractResult.IsMatch)
            {
                return null;
            }

            return extractResult.Matches[0].Value;
        }
    }
}

#endif