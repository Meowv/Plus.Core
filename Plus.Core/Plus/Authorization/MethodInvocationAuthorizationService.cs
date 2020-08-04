using Microsoft.AspNetCore.Authorization;
using Plus.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Plus.Authorization
{
    public class MethodInvocationAuthorizationService : IMethodInvocationAuthorizationService, ITransientDependency
    {
        private readonly IPlusAuthorizationPolicyProvider _PlusAuthorizationPolicyProvider;
        private readonly IPlusAuthorizationService _PlusAuthorizationService;

        public MethodInvocationAuthorizationService(
            IPlusAuthorizationPolicyProvider PlusAuthorizationPolicyProvider,
            IPlusAuthorizationService PlusAuthorizationService)
        {
            _PlusAuthorizationPolicyProvider = PlusAuthorizationPolicyProvider;
            _PlusAuthorizationService = PlusAuthorizationService;
        }

        public async Task CheckAsync(MethodInvocationAuthorizationContext context)
        {
            if (AllowAnonymous(context))
            {
                return;
            }

            var authorizationPolicy = await AuthorizationPolicy.CombineAsync(
                _PlusAuthorizationPolicyProvider,
                GetAuthorizationDataAttributes(context.Method)
            );

            if (authorizationPolicy == null)
            {
                return;
            }

            await _PlusAuthorizationService.CheckAsync(authorizationPolicy);
        }

        protected virtual bool AllowAnonymous(MethodInvocationAuthorizationContext context)
        {
            return context.Method.GetCustomAttributes(true).OfType<IAllowAnonymous>().Any();
        }

        protected virtual IEnumerable<IAuthorizeData> GetAuthorizationDataAttributes(MethodInfo methodInfo)
        {
            var attributes = methodInfo
                .GetCustomAttributes(true)
                .OfType<IAuthorizeData>();

            if (methodInfo.IsPublic && methodInfo.DeclaringType != null)
            {
                attributes = attributes
                    .Union(
                        methodInfo.DeclaringType
                            .GetCustomAttributes(true)
                            .OfType<IAuthorizeData>()
                    );
            }

            return attributes;
        }
    }
}