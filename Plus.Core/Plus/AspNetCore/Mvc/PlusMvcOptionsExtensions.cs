#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Plus.AspNetCore.Mvc.Auditing;
using Plus.AspNetCore.Mvc.Conventions;
using Plus.AspNetCore.Mvc.ExceptionHandling;
using Plus.AspNetCore.Mvc.Features;
using Plus.AspNetCore.Mvc.ModelBinding;
using Plus.AspNetCore.Mvc.Response;
using Plus.AspNetCore.Mvc.Uow;
using Plus.AspNetCore.Mvc.Validation;

namespace Plus.AspNetCore.Mvc
{
    internal static class PlusMvcOptionsExtensions
    {
        public static void AddPlus(this MvcOptions options, IServiceCollection services)
        {
            AddConventions(options, services);
            AddActionFilters(options);
            AddPageFilters(options);
            AddModelBinders(options);
            AddMetadataProviders(options, services);
        }

        private static void AddConventions(MvcOptions options, IServiceCollection services)
        {
            options.Conventions.Add(new PlusServiceConventionWrapper(services));
        }

        private static void AddActionFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(PlusAuditActionFilter));
            options.Filters.AddService(typeof(PlusNoContentActionFilter));
            options.Filters.AddService(typeof(PlusFeatureActionFilter));
            options.Filters.AddService(typeof(PlusValidationActionFilter));
            options.Filters.AddService(typeof(PlusUowActionFilter));
            options.Filters.AddService(typeof(PlusExceptionFilter));
        }

        private static void AddPageFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(PlusExceptionPageFilter));
            options.Filters.AddService(typeof(PlusAuditPageFilter));
            options.Filters.AddService(typeof(PlusFeaturePageFilter));
            options.Filters.AddService(typeof(PlusUowPageFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new PlusDateTimeModelBinderProvider());
            options.ModelBinderProviders.Insert(0, new PlusExtraPropertiesDictionaryModelBinderProvider());
        }

        private static void AddMetadataProviders(MvcOptions options, IServiceCollection services)
        {
            options.ModelMetadataDetailsProviders.Add(
                new PlusDataAnnotationAutoLocalizationMetadataDetailsProvider(services)
            );
        }
    }
}

#endif