#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plus.Auditing;
using Plus.Http;
using Plus.Http.ProxyScripting;
using Plus.Minify.Scripts;

namespace Plus.AspNetCore.Mvc.ProxyScripting
{
    [Area("Plus")]
    [Route("Plus/ServiceProxyScript")]
    [DisableAuditing]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PlusServiceProxyScriptController : PlusController
    {
        private readonly IProxyScriptManager _proxyScriptManager;
        private readonly PlusAspNetCoreMvcOptions _options;
        private readonly IJavascriptMinifier _javascriptMinifier;

        public PlusServiceProxyScriptController(IProxyScriptManager proxyScriptManager,
            IOptions<PlusAspNetCoreMvcOptions> options,
            IJavascriptMinifier javascriptMinifier)
        {
            _proxyScriptManager = proxyScriptManager;
            _options = options.Value;
            _javascriptMinifier = javascriptMinifier;
        }

        [HttpGet]
        [Produces(MimeTypes.Application.Javascript, MimeTypes.Text.Plain)]
        public ActionResult GetAll(ServiceProxyGenerationModel model)
        {
            model.Normalize();

            var script = _proxyScriptManager.GetScript(model.CreateOptions());

            return Content(
                _options.MinifyGeneratedScript == true
                    ? _javascriptMinifier.Minify(script)
                    : script,
                MimeTypes.Application.Javascript
            );
        }
    }
}

#endif