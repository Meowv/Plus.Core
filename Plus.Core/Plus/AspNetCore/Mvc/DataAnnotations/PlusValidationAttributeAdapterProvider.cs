#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Plus.Validation;
using System.ComponentModel.DataAnnotations;

namespace Plus.AspNetCore.Mvc.DataAnnotations
{
    public class PlusValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly ValidationAttributeAdapterProvider _defaultAdapter;

        public PlusValidationAttributeAdapterProvider(ValidationAttributeAdapterProvider defaultAdapter)
        {
            _defaultAdapter = defaultAdapter;
        }

        public virtual IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            var type = attribute.GetType();

            if (type == typeof(DynamicStringLengthAttribute))
            {
                return new DynamicStringLengthAttributeAdapter((DynamicStringLengthAttribute)attribute, stringLocalizer);
            }

            if (type == typeof(DynamicMaxLengthAttribute))
            {
                return new DynamicMaxLengthAttributeAdapter((DynamicMaxLengthAttribute)attribute, stringLocalizer);
            }

            return _defaultAdapter.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}

#endif