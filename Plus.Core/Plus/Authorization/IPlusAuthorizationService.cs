using Microsoft.AspNetCore.Authorization;
using Plus.DependencyInjection;
using System.Security.Claims;

namespace Plus.Authorization
{
    public interface IPlusAuthorizationService : IAuthorizationService, IServiceProviderAccessor
    {
        ClaimsPrincipal CurrentPrincipal { get; }
    }
}