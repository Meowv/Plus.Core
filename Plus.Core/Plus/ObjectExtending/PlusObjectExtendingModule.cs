using Plus.Localization;
using Plus.Modularity;
using Plus.Validation;

namespace Plus.ObjectExtending
{
    [DependsOn(
        typeof(PlusLocalizationAbstractionsModule),
        typeof(PlusValidationAbstractionsModule)
        )]
    public class PlusObjectExtendingModule : PlusModule
    {

    }
}