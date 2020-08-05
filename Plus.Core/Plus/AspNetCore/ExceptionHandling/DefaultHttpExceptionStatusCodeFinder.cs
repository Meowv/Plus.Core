#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plus.Authorization;
using Plus.DependencyInjection;
using Plus.Domain.Entities;
using Plus.ExceptionHandling;
using Plus.Validation;
using System;
using System.Net;

namespace Plus.AspNetCore.ExceptionHandling
{
    public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder, ITransientDependency
    {
        protected PlusExceptionHttpStatusCodeOptions Options { get; }

        public DefaultHttpExceptionStatusCodeFinder(
            IOptions<PlusExceptionHttpStatusCodeOptions> options)
        {
            Options = options.Value;
        }

        public virtual HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
        {
            if (exception is IHasErrorCode exceptionWithErrorCode &&
                !exceptionWithErrorCode.Code.IsNullOrWhiteSpace())
            {
                if (Options.ErrorCodeToHttpStatusCodeMappings.TryGetValue(exceptionWithErrorCode.Code, out var status))
                {
                    return status;
                }
            }

            if (exception is PlusAuthorizationException)
            {
                return httpContext.User.Identity.IsAuthenticated
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            //TODO: Handle SecurityException..?

            if (exception is PlusValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            if (exception is NotImplementedException)
            {
                return HttpStatusCode.NotImplemented;
            }

            if (exception is IBusinessException)
            {
                return HttpStatusCode.Forbidden;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}

#endif