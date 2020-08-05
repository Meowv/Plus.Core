using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.Validation;

namespace Plus.FluentValidation
{
    [DependsOn(
        typeof(PlusValidationModule)
        )]
    public class PlusFluentValidationModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddConventionalRegistrar(new PlusFluentValidationConventionalRegistrar());
        }
    }
}