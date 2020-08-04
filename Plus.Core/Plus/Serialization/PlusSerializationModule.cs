using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.Reflection;

namespace Plus.Serialization
{
    public class PlusSerializationModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnExposing(onServiceExposingContext =>
            {
                //Register types for IObjectSerializer<T> if implements
                onServiceExposingContext.ExposedTypes.AddRange(
                    ReflectionHelper.GetImplementedGenericTypes(
                        onServiceExposingContext.ImplementationType,
                        typeof(IObjectSerializer<>)
                    )
                );
            });
        }
    }
}