#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Plus.Timing;
using System;
using System.Linq;

namespace Plus.AspNetCore.Mvc.ModelBinding
{
    public class PlusDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(DateTime) &&
                context.Metadata.ModelType != typeof(DateTime?))
            {
                return null;
            }

            if (context.Metadata.ContainerType == null)
            {
                if (context.Metadata is DefaultModelMetadata defaultModelMetadata &&
                    defaultModelMetadata.Attributes.Attributes.All(x => x.GetType() != typeof(DisableDateTimeNormalizationAttribute)))
                {
                    return new PlusDateTimeModelBinder(context);
                }
            }
            else
            {
                var dateNormalizationDisabledForClass =
                    context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                    .GetProperty(context.Metadata.PropertyName)
                    ?.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

                if (!dateNormalizationDisabledForClass &&
                    dateNormalizationDisabledForProperty != null &&
                    !dateNormalizationDisabledForProperty.Value)
                {
                    return new PlusDateTimeModelBinder(context);
                }
            }

            return null;
        }
    }
}

#endif