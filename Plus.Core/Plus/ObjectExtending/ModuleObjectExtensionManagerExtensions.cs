using JetBrains.Annotations;
using Plus.ObjectExtending.Modularity;

namespace Plus.ObjectExtending
{
    public static class ModuleObjectExtensionManagerExtensions
    {
        private const string ObjectExtensionManagerConfigurationKey = "_Modules";

        public static ModuleExtensionConfigurationDictionary Modules(
            [NotNull] this ObjectExtensionManager objectExtensionManager)
        {
            Check.NotNull(objectExtensionManager, nameof(objectExtensionManager));

            return objectExtensionManager.Configuration.GetOrAdd(
                ObjectExtensionManagerConfigurationKey,
                _ => new ModuleExtensionConfigurationDictionary()
            ) as ModuleExtensionConfigurationDictionary;
        }
    }
}