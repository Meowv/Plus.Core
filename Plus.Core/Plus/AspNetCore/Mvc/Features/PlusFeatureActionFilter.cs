#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Plus.Aspects;
using Plus.DependencyInjection;
using Plus.Features;
using System;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Features
{
    public class PlusFeatureActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationAuthorizationService;

        public PlusFeatureActionFilter(IMethodInvocationFeatureCheckerService methodInvocationAuthorizationService)
        {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            using (PlusCrossCuttingConcerns.Applying(context.Controller, PlusCrossCuttingConcerns.FeatureChecking))
            {
                await _methodInvocationAuthorizationService.CheckAsync(
                    new MethodInvocationFeatureCheckerContext(methodInfo)
                );

                await next();
            }
        }
    }
}

#endif