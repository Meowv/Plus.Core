#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Plus.AspNetCore.Mvc;
using Plus.AspNetCore.Uow;
using Plus.DependencyInjection;
using Plus.ExceptionHandling;
using Plus.Http;
using Plus.Json;
using System;
using System.Threading.Tasks;

namespace Plus.AspNetCore.ExceptionHandling
{
    public class PlusExceptionHandlingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ILogger<PlusUnitOfWorkMiddleware> _logger;

        private readonly Func<object, Task> _clearCacheHeadersDelegate;

        public PlusExceptionHandlingMiddleware(ILogger<PlusUnitOfWorkMiddleware> logger)
        {
            _logger = logger;

            _clearCacheHeadersDelegate = ClearCacheHeaders;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // We can't do anything if the response has already started, just abort.
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("An exception occurred, but response has already started!");
                    throw;
                }

                if (context.Items["_PlusActionInfo"] is PlusActionInfoInHttpContext actionInfo)
                {
                    if (actionInfo.IsObjectResult) //TODO: Align with PlusExceptionFilter.ShouldHandleException!
                    {
                        await HandleAndWrapException(context, ex);
                        return;
                    }
                }

                throw;
            }
        }

        private async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
        {
            _logger.LogException(exception);

            var errorInfoConverter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter>();
            var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder>();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer>();

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCodeFinder.GetStatusCode(httpContext, exception);
            httpContext.Response.OnStarting(_clearCacheHeadersDelegate, httpContext.Response);
            httpContext.Response.Headers.Add(PlusHttpConsts.PlusErrorFormat, "true");

            await httpContext.Response.WriteAsync(
                jsonSerializer.Serialize(
                    new RemoteServiceErrorResponse(
                        errorInfoConverter.Convert(exception)
                    )
                )
            );

            await httpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier>()
                .NotifyAsync(
                    new ExceptionNotificationContext(exception)
                );
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }
    }
}

#endif