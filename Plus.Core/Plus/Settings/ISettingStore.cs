using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.Settings
{
    public interface ISettingStore
    {
        Task<string> GetOrNullAsync(
            [NotNull] string name,
            [CanBeNull] string providerName,
            [CanBeNull] string providerKey
        );
    }
}