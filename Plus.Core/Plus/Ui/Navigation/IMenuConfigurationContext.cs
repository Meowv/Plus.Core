using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Plus.DependencyInjection;

namespace Plus.UI.Navigation
{
    public interface IMenuConfigurationContext : IServiceProviderAccessor
    {
        ApplicationMenu Menu { get; }

        IAuthorizationService AuthorizationService { get; }

        IStringLocalizerFactory StringLocalizerFactory { get; }
    }
}