using JetBrains.Annotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Plus.IdentityModel
{
    //TODO: Re-consider this interface!
    public interface IIdentityModelAuthenticationService
    {
        Task<bool> TryAuthenticateAsync(
            [NotNull] HttpClient client,
            string identityClientName = null);

        Task<string> GetAccessTokenAsync(
            IdentityClientConfiguration configuration);
    }
}