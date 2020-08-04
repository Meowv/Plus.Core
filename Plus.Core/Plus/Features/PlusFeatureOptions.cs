using Plus.Collections;

namespace Plus.Features
{
    public class PlusFeatureOptions
    {
        public ITypeList<IFeatureDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<IFeatureValueProvider> ValueProviders { get; }

        public PlusFeatureOptions()
        {
            DefinitionProviders = new TypeList<IFeatureDefinitionProvider>();
            ValueProviders = new TypeList<IFeatureValueProvider>();
        }
    }
}