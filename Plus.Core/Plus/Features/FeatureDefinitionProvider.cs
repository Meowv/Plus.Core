using Plus.DependencyInjection;

namespace Plus.Features
{
    public abstract class FeatureDefinitionProvider : IFeatureDefinitionProvider, ITransientDependency
    {
        public abstract void Define(IFeatureDefinitionContext context);
    }
}