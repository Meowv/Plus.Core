using System.Threading.Tasks;

namespace Plus.Http.Client.Authentication
{
    public interface IRemoteServiceHttpClientAuthenticator
    {
        Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context);
    }
}