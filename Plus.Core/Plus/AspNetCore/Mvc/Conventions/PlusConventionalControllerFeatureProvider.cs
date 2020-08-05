#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Plus.AspNetCore.Mvc.Conventions
{
    public class PlusConventionalControllerFeatureProvider : ControllerFeatureProvider
    {
        private readonly IPlusApplication _application;

        public PlusConventionalControllerFeatureProvider(IPlusApplication application)
        {
            _application = application;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            //TODO: Move this to a lazy loaded field for efficiency.
            if (_application.ServiceProvider == null)
            {
                return false;
            }

            var configuration = _application.ServiceProvider
                .GetRequiredService<IOptions<PlusAspNetCoreMvcOptions>>().Value
                .ConventionalControllers
                .ConventionalControllerSettings
                .GetSettingOrNull(typeInfo.AsType());

            return configuration != null;
        }
    }
}

#endif