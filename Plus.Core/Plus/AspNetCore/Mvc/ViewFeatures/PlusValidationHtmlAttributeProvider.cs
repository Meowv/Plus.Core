#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Plus.AspNetCore.Mvc.ModelBinding;
using Plus.AspNetCore.Mvc.Validation;
using Plus.DependencyInjection;
using Plus.Localization;
using Plus.ObjectExtending;
using Plus.Validation.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Plus.AspNetCore.Mvc.ViewFeatures
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ValidationHtmlAttributeProvider))]
    public class PlusValidationHtmlAttributeProvider
        : DefaultValidationHtmlAttributeProvider, ISingletonDependency
    {
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly IStringLocalizerFactory _stringLocalizerFactory;
        private readonly IStringLocalizer<PlusValidationResource> _validationStringLocalizer;
        private readonly IValidationAttributeAdapterProvider _validationAttributeAdapterProvider;

        public PlusValidationHtmlAttributeProvider(
            IOptions<MvcViewOptions> optionsAccessor,
            IModelMetadataProvider metadataProvider,
            ClientValidatorCache clientValidatorCache,
            IValidationAttributeAdapterProvider validationAttributeAdapterProvider,
            IStringLocalizerFactory stringLocalizerFactory,
            IStringLocalizer<PlusValidationResource> validationStringLocalizer)
            : base(
                optionsAccessor,
                metadataProvider,
                clientValidatorCache)
        {
            _metadataProvider = metadataProvider;
            _validationAttributeAdapterProvider = validationAttributeAdapterProvider;
            _stringLocalizerFactory = stringLocalizerFactory;
            _validationStringLocalizer = validationStringLocalizer;
        }

        public override void AddValidationAttributes(
            ViewContext viewContext,
            ModelExplorer modelExplorer,
            IDictionary<string, string> attributes)
        {
            base.AddValidationAttributes(viewContext, modelExplorer, attributes);
            AddExtraPropertyValidationsAttributes(viewContext, modelExplorer, attributes);
        }

        protected virtual void AddExtraPropertyValidationsAttributes(ViewContext viewContext, ModelExplorer modelExplorer, IDictionary<string, string> attributes)
        {
            var nameAttribute = attributes.GetOrDefault("name");
            if (nameAttribute == null)
            {
                return;
            }

            var extraPropertyName = ExtraPropertyBindingHelper.ExtractExtraPropertyName(nameAttribute);
            if (extraPropertyName == null)
            {
                return;
            }

            //TODO: containerName can be null on controller actions..?
            var containerName = ExtraPropertyBindingHelper.ExtractContainerName(nameAttribute);
            if (containerName == null)
            {
                return;
            }

            if (modelExplorer.Container?.ModelType == null)
            {
                return;
            }

            var extensibleObjectType = modelExplorer.Container.ModelType
                .GetProperty(containerName, BindingFlags.Instance | BindingFlags.Public)
                ?.PropertyType;
            if (extensibleObjectType == null)
            {
                return;
            }

            var extensionPropertyInfo = ObjectExtensionManager.Instance.GetPropertyOrNull(
                extensibleObjectType,
                extraPropertyName
            );

            if (extensionPropertyInfo == null)
            {
                return;
            }

            if (modelExplorer.Metadata is DefaultModelMetadata metadata)
            {
                metadata.DisplayMetadata.DisplayName =
                    () => extensionPropertyInfo.GetLocalizedDisplayName(_stringLocalizerFactory);
            }

            foreach (var validationAttribute in extensionPropertyInfo.GetValidationAttributes())
            {
                var validationContext = new ClientModelValidationContext(
                    viewContext,
                    modelExplorer.Metadata,
                    _metadataProvider,
                    attributes
                );

                if (validationAttribute.ErrorMessage == null)
                {
                    ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
                }

                var validationAttributeAdapter = _validationAttributeAdapterProvider.GetAttributeAdapter(
                        validationAttribute,
                        _validationStringLocalizer
                    );

                validationAttributeAdapter?.AddValidation(validationContext);
            }
        }
    }
}

#endif