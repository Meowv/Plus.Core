using System.Threading.Tasks;

namespace Plus.Authorization
{
    public class AlwaysAllowMethodInvocationAuthorizationService : IMethodInvocationAuthorizationService
    {
        public Task CheckAsync(MethodInvocationAuthorizationContext context)
        {
            return Task.CompletedTask;
        }
    }
}