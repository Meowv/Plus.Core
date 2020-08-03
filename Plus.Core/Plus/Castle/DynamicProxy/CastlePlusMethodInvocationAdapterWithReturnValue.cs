using Castle.DynamicProxy;
using Plus.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace Plus.Castle.DynamicProxy
{
    public class CastlePlusMethodInvocationAdapterWithReturnValue<TResult> : CastlePlusMethodInvocationAdapterBase, IPlusMethodInvocation
    {
        protected IInvocationProceedInfo ProceedInfo { get; }
        protected Func<IInvocation, IInvocationProceedInfo, Task<TResult>> Proceed { get; }

        public CastlePlusMethodInvocationAdapterWithReturnValue(IInvocation invocation,
            IInvocationProceedInfo proceedInfo,
            Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
            : base(invocation)
        {
            ProceedInfo = proceedInfo;
            Proceed = proceed;
        }

        public override async Task ProceedAsync()
        {
            ReturnValue = await Proceed(Invocation, ProceedInfo);
        }
    }
}