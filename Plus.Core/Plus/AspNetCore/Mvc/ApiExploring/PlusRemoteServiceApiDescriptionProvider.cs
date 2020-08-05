#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Plus.AspNetCore.Mvc.ApiExploring
{
    public class PlusRemoteServiceApiDescriptionProvider : IApiDescriptionProvider, ITransientDependency
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly MvcOptions _mvcOptions;
        private readonly PlusRemoteServiceApiDescriptionProviderOptions _options;

        public PlusRemoteServiceApiDescriptionProvider(
            IModelMetadataProvider modelMetadataProvider,
            IOptions<MvcOptions> mvcOptionsAccessor,
            IOptions<PlusRemoteServiceApiDescriptionProviderOptions> optionsAccessor)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _mvcOptions = mvcOptionsAccessor.Value;
            _options = optionsAccessor.Value;
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        /// <summary>
        /// The order -999 ensures that this provider is executed right after the
        /// Microsoft.AspNetCore.Mvc.ApiExplorer.DefaultApiDescriptionProvider.
        /// </summary>
        public int Order => -999;

        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var apiResponseType in GetApiResponseTypes())
            {
                foreach (var result in context.Results.Where(x => IsRemoteService(x.ActionDescriptor)))
                {
                    var actionProducesResponseTypeAttributes =
                        ReflectionHelper.GetAttributesOfMemberOrDeclaringType<ProducesResponseTypeAttribute>(
                            result.ActionDescriptor.GetMethodInfo());
                    if (actionProducesResponseTypeAttributes.Any(x => x.StatusCode == apiResponseType.StatusCode))
                    {
                        continue;
                    }

                    result.SupportedResponseTypes.AddIfNotContains(x => x.StatusCode == apiResponseType.StatusCode,
                        () => apiResponseType);
                }
            }
        }

        protected virtual IEnumerable<ApiResponseType> GetApiResponseTypes()
        {
            foreach (var apiResponse in _options.SupportedResponseTypes)
            {
                apiResponse.ModelMetadata = _modelMetadataProvider.GetMetadataForType(apiResponse.Type);

                foreach (var responseTypeMetadataProvider in _mvcOptions.OutputFormatters.OfType<IApiResponseTypeMetadataProvider>())
                {
                    var formatterSupportedContentTypes = responseTypeMetadataProvider.GetSupportedContentTypes(null, apiResponse.Type);
                    if (formatterSupportedContentTypes == null)
                    {
                        continue;
                    }

                    foreach (var formatterSupportedContentType in formatterSupportedContentTypes)
                    {
                        apiResponse.ApiResponseFormats.Add(new ApiResponseFormat
                        {
                            Formatter = (IOutputFormatter)responseTypeMetadataProvider,
                            MediaType = formatterSupportedContentType
                        });
                    }
                }
            }

            return _options.SupportedResponseTypes;
        }

        protected virtual bool IsRemoteService(ActionDescriptor actionDescriptor)
        {
            var remoteServiceAttr = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<RemoteServiceAttribute>(actionDescriptor.GetMethodInfo());
            return remoteServiceAttr != null && remoteServiceAttr.IsEnabled;
        }
    }
}

#endif