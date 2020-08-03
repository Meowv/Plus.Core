using Castle.DynamicProxy;
using Plus.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace Plus.Castle.DynamicProxy
{
    public class CastleAsyncPlusInterceptorAdapter<TInterceptor> : AsyncInterceptorBase
        where TInterceptor : IPlusInterceptor
    {
        private readonly TInterceptor _PlusInterceptor;

        public CastleAsyncPlusInterceptorAdapter(TInterceptor PlusInterceptor)
        {
            _PlusInterceptor = PlusInterceptor;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            await _PlusInterceptor.InterceptAsync(
                new CastlePlusMethodInvocationAdapter(invocation, proceedInfo, proceed)
            );
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var adapter = new CastlePlusMethodInvocationAdapterWithReturnValue<TResult>(invocation, proceedInfo, proceed);

            await _PlusInterceptor.InterceptAsync(
                adapter
            );

            return (TResult)adapter.ReturnValue;
        }
    }
}