#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations
{
    [Area("Plus")]
    [RemoteService(Name = "Plus")]
    [Route("api/Plus/application-configuration")]
    public class PlusApplicationConfigurationController : PlusController, IPlusApplicationConfigurationAppService
    {
        private readonly IPlusApplicationConfigurationAppService _applicationConfigurationAppService;

        public PlusApplicationConfigurationController(
            IPlusApplicationConfigurationAppService applicationConfigurationAppService)
        {
            _applicationConfigurationAppService = applicationConfigurationAppService;
        }

        [HttpGet]
        public async Task<ApplicationConfigurationDto> GetAsync()
        {
            return await _applicationConfigurationAppService.GetAsync();
        }
    }
}

#endif