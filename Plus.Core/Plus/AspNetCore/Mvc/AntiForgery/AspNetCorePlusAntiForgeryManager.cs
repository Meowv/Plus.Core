#if NETCOREAPP3_1

using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;

namespace Plus.AspNetCore.Mvc.AntiForgery
{
    public class AspNetCorePlusAntiForgeryManager : IPlusAntiForgeryManager, ITransientDependency
    {
        public PlusAntiForgeryOptions Options { get; }

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        private readonly IAntiforgery _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCorePlusAntiForgeryManager(
            IAntiforgery antiforgery,
            IHttpContextAccessor httpContextAccessor,
            IOptions<PlusAntiForgeryOptions> options)
        {
            _antiforgery = antiforgery;
            _httpContextAccessor = httpContextAccessor;
            Options = options.Value;
        }

        public void SetCookie()
        {
            HttpContext.Response.Cookies.Append(Options.TokenCookieName, GenerateToken());
        }

        public string GenerateToken()
        {
            return _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext).RequestToken;
        }
    }
}

#endif