#if NETCOREAPP3_1

using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Plus.DependencyInjection;
using Plus.Http.Client.Authentication;
using Plus.IdentityModel;
using System.Threading.Tasks;

namespace Plus.Http.Client.IdentityModel.Web
{
    [Dependency(ReplaceServices = true)]
    public class HttpContextIdentityModelRemoteServiceHttpClientAuthenticator : IdentityModelRemoteServiceHttpClientAuthenticator
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public HttpContextIdentityModelRemoteServiceHttpClientAuthenticator(
            IIdentityModelAuthenticationService identityModelAuthenticationService)
            : base(identityModelAuthenticationService)
        {
        }

        public override async Task AuthenticateAsync(RemoteServiceHttpClientAuthenticateContext context)
        {
            if (context.RemoteService.GetUseCurrentAccessToken() != false)
            {
                var accessToken = await GetAccessTokenFromHttpContextOrNullAsync();
                if (accessToken != null)
                {
                    context.Request.SetBearerToken(accessToken);
                    return;
                }
            }

            await base.AuthenticateAsync(context);
        }

        protected virtual async Task<string> GetAccessTokenFromHttpContextOrNullAsync()
        {
            var httpContext = HttpContextAccessor?.HttpContext;
            if (httpContext == null)
            {
                return null;
            }

            return await httpContext.GetTokenAsync("access_token");
        }
    }
}

#endif