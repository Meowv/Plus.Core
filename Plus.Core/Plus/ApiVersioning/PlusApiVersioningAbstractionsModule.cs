using Microsoft.Extensions.DependencyInjection;
using Plus.Modularity;

namespace Plus.ApiVersioning
{
    public class PlusApiVersioningAbstractionsModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IRequestedApiVersion>(NullRequestedApiVersion.Instance);
        }
    }
}