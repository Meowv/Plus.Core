namespace Plus.Features
{
    public interface IFeatureDefinitionProvider
    {
        void Define(IFeatureDefinitionContext context);
    }
}