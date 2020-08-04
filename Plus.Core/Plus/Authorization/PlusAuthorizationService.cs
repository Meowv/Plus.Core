using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.Security.Claims;
using System;
using System.Security.Claims;

namespace Plus.Authorization
{
    [Dependency(ReplaceServices = true)]
    public class PlusAuthorizationService : DefaultAuthorizationService, IPlusAuthorizationService, ITransientDependency
    {
        public IServiceProvider ServiceProvider { get; }

        public ClaimsPrincipal CurrentPrincipal => _currentPrincipalAccessor.Principal;

        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;

        public PlusAuthorizationService(
            IAuthorizationPolicyProvider policyProvider,
            IAuthorizationHandlerProvider handlers,
            ILogger<DefaultAuthorizationService> logger,
            IAuthorizationHandlerContextFactory contextFactory,
            IAuthorizationEvaluator evaluator,
            IOptions<AuthorizationOptions> options,
            ICurrentPrincipalAccessor currentPrincipalAccessor,
            IServiceProvider serviceProvider)
            : base(
                policyProvider,
                handlers,
                logger,
                contextFactory,
                evaluator,
                options)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            ServiceProvider = serviceProvider;
        }
    }
}