#if NETCOREAPP3_1

using JetBrains.Annotations;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plus;
using Plus.AspNetCore.Auditing;
using Plus.AspNetCore.ExceptionHandling;
using Plus.AspNetCore.Security.Claims;
using Plus.AspNetCore.Tracing;
using Plus.AspNetCore.Uow;
using Plus.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class PlusApplicationBuilderExtensions
    {
        private const string ExceptionHandlingMiddlewareMarker = "_PlusExceptionHandlingMiddleware_Added";

        public static void InitializeApplication([NotNull] this IApplicationBuilder app)
        {
            Check.NotNull(app, nameof(app));

            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
            var application = app.ApplicationServices.GetRequiredService<IPlusApplicationWithExternalServiceProvider>();
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                application.Shutdown();
            });

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                application.Dispose();
            });

            application.Initialize(app.ApplicationServices);
        }

        public static IApplicationBuilder UseAuditing(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<PlusAuditingMiddleware>();
        }

        public static IApplicationBuilder UseUnitOfWork(this IApplicationBuilder app)
        {
            return app
                .UsePlusExceptionHandling()
                .UseMiddleware<PlusUnitOfWorkMiddleware>();
        }

        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<PlusCorrelationIdMiddleware>();
        }

        public static IApplicationBuilder UsePlusRequestLocalization(this IApplicationBuilder app,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            app.ApplicationServices
                .GetRequiredService<IPlusRequestLocalizationOptionsProvider>()
                .InitLocalizationOptions(optionsAction);

            return app.UseMiddleware<PlusRequestLocalizationMiddleware>();
        }

        public static IApplicationBuilder UsePlusExceptionHandling(this IApplicationBuilder app)
        {
            if (app.Properties.ContainsKey(ExceptionHandlingMiddlewareMarker))
            {
                return app;
            }

            app.Properties[ExceptionHandlingMiddlewareMarker] = true;
            return app.UseMiddleware<PlusExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder UsePlusClaimsMap(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PlusClaimsMapMiddleware>();
        }
    }
}

#endif