using Plus.IdentityModel;
using Plus.Modularity;

namespace Plus.Http.Client.IdentityModel
{
    [DependsOn(
        typeof(PlusHttpClientModule),
        typeof(PlusIdentityModelModule)
        )]
    public class PlusHttpClientIdentityModelModule : PlusModule
    {

    }
}