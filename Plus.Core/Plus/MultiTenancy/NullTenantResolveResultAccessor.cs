using Plus.DependencyInjection;

namespace Plus.MultiTenancy
{
    public class NullTenantResolveResultAccessor : ITenantResolveResultAccessor, ISingletonDependency
    {
        public TenantResolveResult Result
        {
            get => null;
            set { }
        }
    }
}