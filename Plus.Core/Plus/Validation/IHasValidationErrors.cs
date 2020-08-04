using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Validation
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}