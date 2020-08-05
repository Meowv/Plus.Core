using FluentValidation;
using Plus.DependencyInjection;
using Plus.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plus.FluentValidation
{
    public class FluentObjectValidationContributor : IObjectValidationContributor, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentObjectValidationContributor(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddErrors(ObjectValidationContext context)
        {
            var serviceType = typeof(IValidator<>).MakeGenericType(context.ValidatingObject.GetType());
            if (!(_serviceProvider.GetService(serviceType) is IValidator validator))
            {
                return;
            }

            var result = validator.Validate((IValidationContext)context.ValidatingObject);
            if (!result.IsValid)
            {
                context.Errors.AddRange(
                    result.Errors.Select(
                        error =>
                            new ValidationResult(error.ErrorMessage, new[] { error.PropertyName })
                    )
                );
            }
        }
    }
}