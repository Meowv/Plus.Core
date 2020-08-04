using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.Authorization.Permissions
{
    public abstract class PermissionValueProvider : IPermissionValueProvider, ITransientDependency
    {
        public abstract string Name { get; }

        protected IPermissionStore PermissionStore { get; }

        protected PermissionValueProvider(IPermissionStore permissionStore)
        {
            PermissionStore = permissionStore;
        }

        public abstract Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context);
    }
}