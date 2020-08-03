using System.Threading.Tasks;

namespace Plus.DynamicProxy
{
    public interface IPlusInterceptor
    {
        Task InterceptAsync(IPlusMethodInvocation invocation);
    }
}
