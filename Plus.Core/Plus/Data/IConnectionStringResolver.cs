using JetBrains.Annotations;

namespace Plus.Data
{
    public interface IConnectionStringResolver
    {
        [NotNull]
        string Resolve(string connectionStringName = null);
    }
}