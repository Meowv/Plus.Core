using JetBrains.Annotations;

namespace Plus.TextTemplating.VirtualFiles
{
    public interface ILocalizedTemplateContentReader
    {
        public string GetContentOrNull([CanBeNull] string culture);
    }
}