namespace Plus.Modularity
{
    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
    {
        public virtual void Initialize(ApplicationInitializationContext context, IPlusModule module)
        {
        }

        public virtual void Shutdown(ApplicationShutdownContext context, IPlusModule module)
        {
        }
    }
}