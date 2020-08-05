#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace Plus.AspNetCore.Mvc.Conventions
{
    public class PlusConventionalApiControllerSpecification : IApiControllerSpecification
    {
        private readonly PlusAspNetCoreMvcOptions _options;

        public PlusConventionalApiControllerSpecification(IOptions<PlusAspNetCoreMvcOptions> options)
        {
            _options = options.Value;
        }

        public bool IsSatisfiedBy(ControllerModel controller)
        {
            var configuration = _options
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull(controller.ControllerType.AsType());

            return configuration != null;
        }
    }
}

#endif