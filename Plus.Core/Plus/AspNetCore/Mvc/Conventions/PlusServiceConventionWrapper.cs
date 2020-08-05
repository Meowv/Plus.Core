#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Plus.DependencyInjection;
using System;

namespace Plus.AspNetCore.Mvc.Conventions
{
    [DisableConventionalRegistration]
    public class PlusServiceConventionWrapper : IApplicationModelConvention
    {
        private readonly Lazy<IPlusServiceConvention> _convention;

        public PlusServiceConventionWrapper(IServiceCollection services)
        {
            _convention = services.GetRequiredServiceLazy<IPlusServiceConvention>();
        }

        public void Apply(ApplicationModel application)
        {
            _convention.Value.Apply(application);
        }
    }
}

#endif