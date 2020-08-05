#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Plus.Http.Modeling;

namespace Plus.AspNetCore.Mvc.ApiExploring
{
    [Area("Plus")]
    [RemoteService(Name = "Plus")]
    [Route("api/Plus/api-definition")]
    public class PlusApiDefinitionController : PlusController, IRemoteService
    {
        private readonly IApiDescriptionModelProvider _modelProvider;

        public PlusApiDefinitionController(IApiDescriptionModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
        }

        [HttpGet]
        public ApplicationApiDescriptionModel Get(ApplicationApiDescriptionModelRequestDto model)
        {
            return _modelProvider.CreateApiModel(model);
        }
    }
}

#endif