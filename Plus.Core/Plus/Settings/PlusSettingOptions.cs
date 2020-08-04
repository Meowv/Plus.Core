using Plus.Collections;

namespace Plus.Settings
{
    public class PlusSettingOptions
    {
        public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<ISettingValueProvider> ValueProviders { get; }

        public PlusSettingOptions()
        {
            DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
            ValueProviders = new TypeList<ISettingValueProvider>();
        }
    }
}