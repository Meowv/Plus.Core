#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Plus.Aspects;
using Plus.DependencyInjection;
using Plus.Features;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Features
{
    public class PlusFeaturePageFilter : IAsyncPageFilter, ITransientDependency
    {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationAuthorizationService;

        public PlusFeaturePageFilter(IMethodInvocationFeatureCheckerService methodInvocationAuthorizationService)
        {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction())
            {
                await next();
                return;
            }

            var methodInfo = context.HandlerMethod.MethodInfo;

            using (PlusCrossCuttingConcerns.Applying(context.HandlerInstance, PlusCrossCuttingConcerns.FeatureChecking))
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