using JetBrains.Annotations;

namespace Plus.Localization
{
    public interface IHasNameWithLocalizableDisplayName
    {
        [NotNull]
        public string Name { get; }

        [CanBeNull]
        public ILocalizableString DisplayName { get; }
    }
}