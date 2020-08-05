#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Plus.AspNetCore.ExceptionHandling;
using Plus.DependencyInjection;
using Plus.ExceptionHandling;
using Plus.Http;
using Plus.Json;
using System;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.ExceptionHandling
{
    public class PlusExceptionPageFilter : IAsyncPageFilter, ITransientDependency
    {
        public ILogger<PlusExceptionPageFilter> Logger { get; set; }

        private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
        private readonly IJsonSerializer _jsonSerializer;

        public PlusExceptionPageFilter(
            IExceptionToErrorInfoConverter errorInfoConverter,
            IHttpExceptionStatusCodeFinder statusCodeFinder,
            IJsonSerializer jsonSerializer)
        {
            _errorInfoConverter = errorInfoConverter;
            _statusCodeFinder = statusCodeFinder;
            _jsonSerializer = jsonSerializer;

            Logger = NullLogger<PlusExceptionPageFilter>.Instance;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null || !ShouldHandleException(context))
            {
                await next();
                return;
            }

            var pageHandlerExecutedContext = await next();
            if (pageHandlerExecutedContext.Exception == null)
            {
                return; ;
            }

            await HandleAndWrapException(pageHandlerExecutedContext);
        }

        protected virtual bool ShouldHandleException(PageHandlerExecutingContext context)
        {
            //TODO: Create DontWrap attribute to control wrapping..?

            if (context.ActionDescriptor.IsPageAction() &&
                ActionResultHelper.IsObjectResult(context.HandlerMethod.MethodInfo.ReturnType, typeof(void)))
            {
                return true;
            }

            if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
            {
                return true;
            }

            if (context.HttpContext.Request.IsAjax())
            {
                return true;
            }

            return false;
        }

        protected virtual async Task HandleAndWrapException(PageHandlerExecutedContext context)
        {
            //TODO: Trigger an PlusExceptionHandled event or something like that.

            context.HttpContext.Response.Headers.Add(PlusHttpConsts.PlusErrorFormat, "true");
            context.HttpContext.Response.StatusCode = (int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception);

            var remoteServiceErrorInfo = _errorInfoConverter.Convert(context.Exception);

            context.Result = new ObjectResult(new RemoteServiceErrorResponse(remoteServiceErrorInfo));

            var logLevel = context.Exception.GetLogLevel();

            Logger.LogWithLevel(logLevel, $"---------- {nameof(RemoteServiceErrorInfo)} ----------");
            Logger.LogWithLevel(logLevel, _jsonSerializer.Serialize(remoteServiceErrorInfo, indented: true));
            Logger.LogException(context.Exception, logLevel);

            await context.HttpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier>()
                .NotifyAsync(
                    new ExceptionNotificationContext(context.Exception)
                );

            context.Exception = null; //Handled!
        }
    }
}

#endif