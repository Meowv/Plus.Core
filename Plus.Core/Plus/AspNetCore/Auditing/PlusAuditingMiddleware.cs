#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Plus.Auditing;
using Plus.DependencyInjection;
using Plus.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Auditing
{
    public class PlusAuditingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IAuditingManager _auditingManager;

        protected PlusAuditingOptions Options { get; }
        protected ICurrentUser CurrentUser { get; }

        public PlusAuditingMiddleware(
            IAuditingManager auditingManager,
            ICurrentUser currentUser,
            IOptions<PlusAuditingOptions> options)
        {
            _auditingManager = auditingManager;

            CurrentUser = currentUser;
            Options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            bool hasError = false;
            using (var scope = _auditingManager.BeginScope())
            {
                try
                {
                    await next(context);
                    if (_auditingManager.Current.Log.Exceptions.Any())
                    {
                        hasError = true;
                    }
                }
                catch (Exception)
                {
                    hasError = true;
                    throw;
                }
                finally
                {
                    if (ShouldWriteAuditLog(context, hasError))
                    {
                        await scope.SaveAsync();
                    }
                }
            }
        }

        private bool ShouldWriteAuditLog(HttpContext httpContext, bool hasError = false)
        {
            if (!Options.IsEnabled)
            {
                return false;
            }

            if (Options.AlwaysLogOnException && hasError)
            {
                return true;
            }

            if (!Options.IsEnabledForAnonymousUsers && !CurrentUser.IsAuthenticated)
            {
                return false;
            }

            if (!Options.IsEnabledForGetRequests &&
                string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}

#endif