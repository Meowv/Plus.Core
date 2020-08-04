using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;
using Plus.ObjectExtending;
using Plus.Uow;
using System;
using System.Collections.Generic;

namespace Plus.Data
{
    [DependsOn(
        typeof(PlusObjectExtendingModule),
        typeof(PlusUnitOfWorkModule)
    )]
    public class PlusDataModule : PlusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutoAddDataSeedContributors(context.Services);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<PlusDbConnectionOptions>(configuration);

            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
        }

        private static void AutoAddDataSeedContributors(IServiceCollection services)
        {
            var contributors = new List<Type>();

            services.OnRegistred(context =>
            {
                if (typeof(IDataSeedContributor).IsAssignableFrom(context.ImplementationType))
                {
                    contributors.Add(context.ImplementationType);
                }
            });

            services.Configure<PlusDataSeedOptions>(options =>
            {
                options.Contributors.AddIfNotContains(contributors);
            });
        }
    }
}