using Plus.Http.ProxyScripting.Configuration;
using Plus.Http.ProxyScripting.Generators.JQuery;
using Plus.Json;
using Plus.Minify;
using Plus.Modularity;

namespace Plus.Http
{
    [DependsOn(typeof(PlusHttpAbstractionsModule))]
    [DependsOn(typeof(PlusJsonModule))]
    [DependsOn(typeof(PlusMinifyModule))]
    public class PlusHttpModule : PlusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<PlusApiProxyScriptingOptions>(options =>
            {
                options.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);
            });
        }
    }
}