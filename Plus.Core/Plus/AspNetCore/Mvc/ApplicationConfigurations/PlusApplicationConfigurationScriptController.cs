#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plus.Auditing;
using Plus.Http;
using Plus.Json;
using Plus.Minify.Scripts;
using System.Text;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations
{
    [Area("Plus")]
    [Route("Plus/ApplicationConfigurationScript")]
    [DisableAuditing]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PlusApplicationConfigurationScriptController : PlusController
    {
        private readonly IPlusApplicationConfigurationAppService _configurationAppService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly PlusAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;

        public PlusApplicationConfigurationScriptController(
            IPlusApplicationConfigurationAppService configurationAppService,
            IJsonSerializer jsonSerializer,
            IOptions<PlusAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier)
        {
            _configurationAppService = configurationAppService;
            _jsonSerializer = jsonSerializer;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
        }

        [HttpGet]
        [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public async Task<ActionResult> Get()
        {
            var script = CreatePlusExtendScript(await _configurationAppService.GetAsync());

            return Content(
                _options.MinifyGeneratedScript == true
                    ? _javascriptMinifier.Minify(script)
                    : script,
                MimeTypes.Application.Javascript
            );
        }

        private string CreatePlusExtendScript(ApplicationConfigurationDto config)
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine($"$.extend(true, Plus, {_jsonSerializer.Serialize(config, indented: true)})");
            script.AppendLine();
            script.AppendLine("Plus.event.trigger('Plus.configurationInitialized');");
            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}

#endif