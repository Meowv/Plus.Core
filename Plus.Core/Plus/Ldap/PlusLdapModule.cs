using Microsoft.Extensions.DependencyInjection;
using Plus.Autofac;
using Plus.Modularity;

namespace Plus.Ldap
{
    [DependsOn(
        typeof(PlusAutofacModule)
    )]
    public class PlusLdapModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<PlusLdapOptions>(configuration.GetSection("LDAP"));
        }
    }
}