using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.Features
{
    public interface IFeatureStore
    {
        Task<string> GetOrNullAsync(
            [NotNull] string name,
            [CanBeNull] string providerName,
            [CanBeNull] string providerKey
        );
    }
}