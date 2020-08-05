#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.AspNetCore.Mvc.Validation;
using Plus.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plus.AspNetCore.Mvc.ModelBinding.Metadata
{
    [Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
    [ExposeServices(typeof(IModelMetadataProvider))]
    public class PlusModelMetadataProvider : DefaultModelMetadataProvider
    {
        public PlusModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider)
            : base(detailsProvider)
        {
        }

        public PlusModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider, IOptions<MvcOptions> optionsAccessor)
            : base(detailsProvider, optionsAccessor)
        {
        }

        protected override DefaultMetadataDetails[] CreatePropertyDetails(ModelMetadataIdentity key)
        {
            var details = base.CreatePropertyDetails(key);

            foreach (var detail in details)
            {
                NormalizeMetadataDetail(detail);
            }

            return details;
        }

        private void NormalizeMetadataDetail(DefaultMetadataDetails detail)
        {
            foreach (var validationAttribute in detail.ModelAttributes.Attributes.OfType<ValidationAttribute>())
            {
                NormalizeValidationAttrbute(validationAttribute);
            }
        }

        protected virtual void NormalizeValidationAttrbute(ValidationAttribute validationAttribute)
        {
            if (validationAttribute.ErrorMessage == null)
            {
                ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
            }
        }
    }
}

#endif