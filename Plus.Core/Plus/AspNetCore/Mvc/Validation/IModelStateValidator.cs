#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Plus.Validation;

namespace Plus.AspNetCore.Mvc.Validation
{
    public interface IModelStateValidator
    {
        void Validate(ModelStateDictionary modelState);

        void AddErrors(IPlusValidationResult validationResult, ModelStateDictionary modelState);
    }
}

#endif