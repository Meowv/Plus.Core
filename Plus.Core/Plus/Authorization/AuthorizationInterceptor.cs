using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System.Threading.Tasks;

namespace Plus.Authorization
{
    public class AuthorizationInterceptor : PlusInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationAuthorizationService _methodInvocationAuthorizationService;

        public AuthorizationInterceptor(IMethodInvocationAuthorizationService methodInvocationAuthorizationService)
        {
            _methodInvocationAuthorizationService = methodInvocationAuthorizationService;
        }

        public override async Task InterceptAsync(IPlusMethodInvocation invocation)
        {
            await AuthorizeAsync(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual async Task AuthorizeAsync(IPlusMethodInvocation invocation)
        {
            await _methodInvocationAuthorizationService.CheckAsync(
                new MethodInvocationAuthorizationContext(
                    invocation.Method
                )
            );
        }
    }
}