using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.Auditing;
using Plus.Modularity;
using Plus.ObjectExtending;
using Plus.ObjectMapping;
using System;

namespace Plus.AutoMapper
{
    [DependsOn(
        typeof(PlusObjectMappingModule),
        typeof(PlusObjectExtendingModule),
        typeof(PlusAuditingModule)
        )]
    public class PlusAutoMapperModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper();

            var mapperAccessor = new MapperAccessor();
            context.Services.AddSingleton<IMapperAccessor>(_ => mapperAccessor);
            context.Services.AddSingleton<MapperAccessor>(_ => mapperAccessor);
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            CreateMappings(context.ServiceProvider);
        }

        private void CreateMappings(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<PlusAutoMapperOptions>>().Value;

                void ConfigureAll(IPlusAutoMapperConfigurationContext ctx)
                {
                    foreach (var configurator in options.Configurators)
                    {
                        configurator(ctx);
                    }
                }

                void ValidateAll(IConfigurationProvider config)
                {
                    foreach (var profileType in options.ValidatingProfiles)
                    {
                        config.AssertConfigurationIsValid(((Profile)Activator.CreateInstance(profileType)).ProfileName);
                    }
                }

                var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression =>
                {
                    ConfigureAll(new PlusAutoMapperConfigurationContext(mapperConfigurationExpression, scope.ServiceProvider));
                });

                ValidateAll(mapperConfiguration);

                scope.ServiceProvider.GetRequiredService<MapperAccessor>().Mapper = mapperConfiguration.CreateMapper();
            }
        }
    }
}