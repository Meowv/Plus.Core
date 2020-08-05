#if NETCOREAPP3_1

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Plus.AspNetCore.Auditing;
using Plus.Auditing;
using Plus.Authorization;
using Plus.Domain;
using Plus.Http;
using Plus.Localization;
using Plus.Modularity;
using Plus.Security;
using Plus.UI;
using Plus.Uow;
using Plus.Validation;
using Plus.VirtualFileSystem;

namespace Plus.AspNetCore
{
    [DependsOn(
        typeof(PlusAuditingModule),
        typeof(PlusSecurityModule),
        typeof(PlusVirtualFileSystemModule),
        typeof(PlusUnitOfWorkModule),
        typeof(PlusHttpModule),
        typeof(PlusAuthorizationModule),
        typeof(PlusDddDomainModule),
        typeof(PlusLocalizationModule),
        typeof(PlusUiModule),
        typeof(PlusValidationModule)
        )]
    public class PlusAspNetCoreModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusAuditingOptions>(options =>
            {
                options.Contributors.Add(new AspNetCoreAuditLogContributor());
            });

            AddAspNetServices(context.Services);
            context.Services.AddObjectAccessor<IApplicationBuilder>();

            context.Services.Replace(ServiceDescriptor.Transient<IOptionsFactory<RequestLocalizationOptions>, PlusRequestLocalizationOptionsFactory>());
        }

        private static void AddAspNetServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}

#endif