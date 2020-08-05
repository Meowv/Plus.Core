#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.Tracing;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Tracing
{
    public class PlusCorrelationIdMiddleware : IMiddleware, ITransientDependency
    {
        private readonly PlusCorrelationIdOptions _options;
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public PlusCorrelationIdMiddleware(IOptions<PlusCorrelationIdOptions> options,
            ICorrelationIdProvider correlationIdProvider)
        {
            _options = options.Value;
            _correlationIdProvider = correlationIdProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var correlationId = _correlationIdProvider.Get();

            try
            {
                await next(context);
            }
            finally
            {
                CheckAndSetCorrelationIdOnResponse(context, _options, correlationId);
            }
        }

        protected virtual void CheckAndSetCorrelationIdOnResponse(
            HttpContext httpContext,
            PlusCorrelationIdOptions options,
            string correlationId)
        {
            if (httpContext.Response.HasStarted)
            {
                return;
            }

            if (!options.SetResponseHeader)
            {
                return;
            }

            if (httpContext.Response.Headers.ContainsKey(options.HttpHeaderName))
            {
                return;
            }

            httpContext.Response.Headers[options.HttpHeaderName] = correlationId;
        }
    }
}

#endif