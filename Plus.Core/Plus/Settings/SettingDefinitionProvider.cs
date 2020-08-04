using Plus.DependencyInjection;

namespace Plus.Settings
{
    public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency
    {
        public abstract void Define(ISettingDefinitionContext context);
    }
}