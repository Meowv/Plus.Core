using Plus.Application.Services;
using Plus.Authorization;
using Plus.Domain;
using Plus.Features;
using Plus.Http;
using Plus.Http.Modeling;
using Plus.Modularity;
using Plus.ObjectMapping;
using Plus.Security;
using Plus.Settings;
using Plus.Uow;
using Plus.Validation;
using System.Collections.Generic;

namespace Plus.Application
{
    [DependsOn(
        typeof(PlusDddDomainModule),
        typeof(PlusDddApplicationContractsModule),
        typeof(PlusSecurityModule),
        typeof(PlusObjectMappingModule),
        typeof(PlusValidationModule),
        typeof(PlusAuthorizationModule),
        typeof(PlusHttpAbstractionsModule),
        typeof(PlusSettingsModule),
        typeof(PlusFeaturesModule)
        )]
    public class PlusDddApplicationModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusApiDescriptionModelOptions>(options =>
            {
                options.IgnoredInterfaces.AddIfNotContains(typeof(IRemoteService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IApplicationService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
            });
        }
    }
}