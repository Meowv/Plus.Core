using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Plus.Autofac;

namespace Plus
{
    public static class PlusAutofacPlusApplicationCreationOptionsExtensions
    {
        public static void UseAutofac(this PlusApplicationCreationOptions options)
        {
            var builder = new ContainerBuilder();
            options.Services.AddObjectAccessor(builder);
            options.Services.AddSingleton((IServiceProviderFactory<ContainerBuilder>)new PlusAutofacServiceProviderFactory(builder));
        }
    }
}