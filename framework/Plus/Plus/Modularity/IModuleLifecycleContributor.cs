using JetBrains.Annotations;
using Plus.DependencyInjection;

namespace Plus.Modularity
{
    public interface IModuleLifecycleContributor : ITransientDependency
    {
        void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] IPlusModule module);

        void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] IPlusModule module);
    }
}
