#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Plus.DependencyInjection;
using Plus.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Plus.AspNetCore.Mvc.Validation
{
    public class ModelStateValidator : IModelStateValidator, ITransientDependency
    {
        public virtual void Validate(ModelStateDictionary modelState)
        {
            var validationResult = new PlusValidationResult();

            AddErrors(validationResult, modelState);

            if (validationResult.Errors.Any())
            {
                throw new PlusValidationException(
                    "ModelState is not valid! See ValidationErrors for details.",
                    validationResult.Errors
                );
            }
        }

        public virtual void AddErrors(IPlusValidationResult validationResult, ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return;
            }

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationResult.Errors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
        }
    }
}

#endif