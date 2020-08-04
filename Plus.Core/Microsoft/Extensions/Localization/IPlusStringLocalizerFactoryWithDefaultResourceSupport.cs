using JetBrains.Annotations;

namespace Microsoft.Extensions.Localization
{
    public interface IPlusStringLocalizerFactoryWithDefaultResourceSupport
    {
        [CanBeNull]
        IStringLocalizer CreateDefaultOrNull();
    }
}