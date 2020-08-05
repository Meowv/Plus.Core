#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Plus.DependencyInjection;
using Plus.MultiTenancy;

namespace Plus.AspNetCore.MultiTenancy
{
    [Dependency(ReplaceServices = true)]
    public class HttpContextTenantResolveResultAccessor : ITenantResolveResultAccessor, ITransientDependency
    {
        public const string HttpContextItemName = "__PlusTenantResolveResult";

        public TenantResolveResult Result
        {
            get => _httpContextAccessor.HttpContext?.Items[HttpContextItemName] as TenantResolveResult;
            set
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return;
                }

                _httpContextAccessor.HttpContext.Items[HttpContextItemName] = value;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTenantResolveResultAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

#endif