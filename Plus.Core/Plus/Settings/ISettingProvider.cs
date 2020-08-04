using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Settings
{
    public interface ISettingProvider
    {
        Task<string> GetOrNullAsync([NotNull] string name);

        Task<List<SettingValue>> GetAllAsync();
    }
}