using Plus.DependencyInjection;
using Plus.Http.Client.Authentication;
using Plus.IdentityModel;
using System.Threading.Tasks;

namespace Plus.Http.Client.IdentityModel
{
    [Dependency(ReplaceServices = true)]
    public class IdentityModelRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ITransientDependency
    {
        protected IIdentityModelAuthenticationService IdentityModelAuthenticationService { get; }

        public IdentityModelRemoteServiceHttpClientAuthenticator(
            IIdentityModelAuthenticationService identityModelAuthenticationService)
        {
            IdentityModelAuthenticationService = identityModelAuthenticationService;
        }

        public virtual async Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context)
        {
            await IdentityModelAuthenticationService.TryAuthenticateAsync(
                context.Client,
                context.RemoteService.GetIdentityClient()
            );
        }
    }
}