using Plus.DependencyInjection;
using Plus.Security.Claims;
using System.Security.Principal;

namespace Plus.Clients
{
    public class CurrentClient : ICurrentClient, ITransientDependency
    {
        public virtual string Id => _principalAccessor.Principal?.FindClientId();

        public virtual bool IsAuthenticated => Id != null;

        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentClient(ICurrentPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }
    }
}