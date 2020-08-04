using Plus.Modularity;
using Plus.Security;

namespace Plus.AspNetCore.Authentication.JwtBearer
{
    [DependsOn(typeof(PlusSecurityModule))]
    public class PlusAspNetCoreAuthenticationJwtBearerModule : PlusModule
    {

    }
}