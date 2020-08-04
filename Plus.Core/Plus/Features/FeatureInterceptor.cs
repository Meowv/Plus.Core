using Plus.Aspects;
using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System.Threading.Tasks;

namespace Plus.Features
{
    public class FeatureInterceptor : PlusInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationFeatureCheckerService _methodInvocationFeatureCheckerService;

        public FeatureInterceptor(
            IMethodInvocationFeatureCheckerService methodInvocationFeatureCheckerService)
        {
            _methodInvocationFeatureCheckerService = methodInvocationFeatureCheckerService;
        }

        public override async Task InterceptAsync(IPlusMethodInvocation invocation)
        {
            if (PlusCrossCuttingConcerns.IsApplied(invocation.TargetObject, PlusCrossCuttingConcerns.FeatureChecking))
            {
                await invocation.ProceedAsync();
                return;
            }

            await CheckFeaturesAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task CheckFeaturesAsync(IPlusMethodInvocation invocation)
        {
            await _methodInvocationFeatureCheckerService.CheckAsync(
                new MethodInvocationFeatureCheckerContext(
                    invocation.Method
                )
            );
        }
    }
}