using System.Threading.Tasks;

namespace Plus.Authorization
{
    public interface IMethodInvocationAuthorizationService
    {
        Task CheckAsync(MethodInvocationAuthorizationContext context);
    }
}