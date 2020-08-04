using System.Collections.Generic;

namespace Plus.Settings
{
    public interface ISettingValueProviderManager
    {
        List<ISettingValueProvider> Providers { get; }
    }
}