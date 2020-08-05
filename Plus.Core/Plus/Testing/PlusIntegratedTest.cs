using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using System;

namespace Plus.Testing
{
    public abstract class PlusIntegratedTest<TStartupModule> : PlusTestBaseWithServiceProvider, IDisposable
        where TStartupModule : IPlusModule
    {
        protected IPlusApplication Application { get; }

        protected override IServiceProvider ServiceProvider => Application.ServiceProvider;

        protected IServiceProvider RootServiceProvider { get; }

        protected IServiceScope TestServiceScope { get; }

        protected PlusIntegratedTest()
        {
            var services = CreateServiceCollection();

            BeforeAddApplication(services);

            var application = services.AddApplication<TStartupModule>(SetPlusApplicationCreationOptions);
            Application = application;

            AfterAddApplication(services);

            RootServiceProvider = CreateServiceProvider(services);
            TestServiceScope = RootServiceProvider.CreateScope();

            application.Initialize(TestServiceScope.ServiceProvider);
        }

        protected virtual IServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }

        protected virtual void BeforeAddApplication(IServiceCollection services)
        {

        }

        protected virtual void SetPlusApplicationCreationOptions(PlusApplicationCreationOptions options)
        {

        }

        protected virtual void AfterAddApplication(IServiceCollection services)
        {

        }

        protected virtual IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            return services.BuildServiceProviderFromFactory();
        }

        public virtual void Dispose()
        {
            Application.Shutdown();
            TestServiceScope.Dispose();
            Application.Dispose();
        }
    }
}