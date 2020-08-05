using Plus.Modularity;
using Plus.Security;

namespace Plus.AspNetCore.Authentication.OAuth
{
    [DependsOn(typeof(PlusSecurityModule))]
    public class PlusAspNetCoreAuthenticationOAuthModule : PlusModule
    {

    }
}