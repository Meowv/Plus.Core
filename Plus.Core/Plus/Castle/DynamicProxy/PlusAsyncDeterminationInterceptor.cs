using Castle.DynamicProxy;
using Plus.DynamicProxy;

namespace Plus.Castle.DynamicProxy
{
    public class PlusAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
        where TInterceptor : IPlusInterceptor
    {
        public PlusAsyncDeterminationInterceptor(TInterceptor PlusInterceptor)
            : base(new CastleAsyncPlusInterceptorAdapter<TInterceptor>(PlusInterceptor))
        {

        }
    }
}