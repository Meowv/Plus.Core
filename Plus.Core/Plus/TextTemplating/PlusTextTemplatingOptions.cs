using Plus.Collections;

namespace Plus.TextTemplating
{
    public class PlusTextTemplatingOptions
    {
        public ITypeList<ITemplateDefinitionProvider> DefinitionProviders { get; }
        public ITypeList<ITemplateContentContributor> ContentContributors { get; }

        public PlusTextTemplatingOptions()
        {
            DefinitionProviders = new TypeList<ITemplateDefinitionProvider>();
            ContentContributors = new TypeList<ITemplateContentContributor>();
        }
    }
}