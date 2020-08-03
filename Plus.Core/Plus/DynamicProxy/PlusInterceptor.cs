using System.Threading.Tasks;

namespace Plus.DynamicProxy
{
    public abstract class PlusInterceptor : IPlusInterceptor
    {
        public abstract Task InterceptAsync(IPlusMethodInvocation invocation);
    }
}