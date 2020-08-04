namespace Microsoft.Extensions.Localization
{
    public static class PlusStringLocalizerFactoryExtensions
    {
        public static IStringLocalizer CreateDefaultOrNull(this IStringLocalizerFactory localizerFactory)
        {
            return (localizerFactory as IPlusStringLocalizerFactoryWithDefaultResourceSupport)
                ?.CreateDefaultOrNull();
        }
    }
}