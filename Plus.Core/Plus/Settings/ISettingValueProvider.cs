using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.Settings
{
    public interface ISettingValueProvider
    {
        string Name { get; }

        Task<string> GetOrNullAsync([NotNull] SettingDefinition setting);
    }
}