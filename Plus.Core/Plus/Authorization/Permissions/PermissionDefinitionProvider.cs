using Plus.DependencyInjection;

namespace Plus.Authorization.Permissions
{
    public abstract class PermissionDefinitionProvider : IPermissionDefinitionProvider, ITransientDependency
    {
        public virtual void PreDefine(IPermissionDefinitionContext context)
        {

        }

        public abstract void Define(IPermissionDefinitionContext context);

        public virtual void PostDefine(IPermissionDefinitionContext context)
        {

        }
    }
}