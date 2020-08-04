using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.Http.Client.Authentication
{
    [Dependency(TryRegister = true)]
    public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
    {
        public Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context)
        {
            return Task.CompletedTask;
        }
    }
}