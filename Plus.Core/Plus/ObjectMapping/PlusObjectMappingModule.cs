using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.Reflection;

namespace Plus.ObjectMapping
{
    public class PlusObjectMappingModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnExposing(onServiceExposingContext =>
            {
                //Register types for IObjectMapper<TSource, TDestination> if implements
                onServiceExposingContext.ExposedTypes.AddRange(
                    ReflectionHelper.GetImplementedGenericTypes(
                        onServiceExposingContext.ImplementationType,
                        typeof(IObjectMapper<,>)
                    )
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(
                typeof(IObjectMapper<>),
                typeof(DefaultObjectMapper<>)
            );
        }
    }
}