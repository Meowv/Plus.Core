using System.Collections.Generic;

namespace Plus.Authorization.Permissions
{
    public interface IPermissionValueProviderManager
    {
        IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
    }
}