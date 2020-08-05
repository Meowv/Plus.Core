#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Plus.DependencyInjection;
using Plus.Threading;
using System.Threading;

namespace Plus.AspNetCore.Threading
{
    [Dependency(ReplaceServices = true)]
    public class HttpContextCancellationTokenProvider : ICancellationTokenProvider, ITransientDependency
    {
        public CancellationToken Token => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}

#endif