#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plus.ApiVersioning;

namespace Plus.AspNetCore.Mvc.Versioning
{
    public class HttpContextRequestedApiVersion : IRequestedApiVersion
    {
        public string Current => _httpContextAccessor.HttpContext?.GetRequestedApiVersion().ToString();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextRequestedApiVersion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

#endif