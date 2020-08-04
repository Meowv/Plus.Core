using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System.Threading.Tasks;

namespace Plus.Validation
{
    public class ValidationInterceptor : PlusInterceptor, ITransientDependency
    {
        private readonly IMethodInvocationValidator _methodInvocationValidator;

        public ValidationInterceptor(IMethodInvocationValidator methodInvocationValidator)
        {
            _methodInvocationValidator = methodInvocationValidator;
        }

        public override async Task InterceptAsync(IPlusMethodInvocation invocation)
        {
            Validate(invocation);
            await invocation.ProceedAsync();
        }

        protected virtual void Validate(IPlusMethodInvocation invocation)
        {
            _methodInvocationValidator.Validate(
                new MethodInvocationValidationContext(
                    invocation.TargetObject,
                    invocation.Method,
                    invocation.Arguments
                )
            );
        }
    }
}