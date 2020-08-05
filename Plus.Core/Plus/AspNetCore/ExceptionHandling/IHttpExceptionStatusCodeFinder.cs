#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Plus.AspNetCore.ExceptionHandling
{
    public interface IHttpExceptionStatusCodeFinder
    {
        HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception);
    }
}

#endif