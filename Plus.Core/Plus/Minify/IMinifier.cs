using JetBrains.Annotations;

namespace Plus.Minify
{
    public interface IMinifier
    {
        string Minify(
            string source,
            [CanBeNull] string fileName = null,
            [CanBeNull] string originalFileName = null);
    }
}