using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plus.Authorization;
using Plus.Authorization.Permissions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddAlwaysAllowAuthorization(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService>());
            services.Replace(ServiceDescriptor.Singleton<IPlusAuthorizationService, AlwaysAllowAuthorizationService>());
            services.Replace(ServiceDescriptor.Singleton<IMethodInvocationAuthorizationService, AlwaysAllowMethodInvocationAuthorizationService>());
            return services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());
        }
    }
}