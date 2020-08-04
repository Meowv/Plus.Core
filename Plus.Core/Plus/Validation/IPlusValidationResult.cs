using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Validation
{
    public interface IPlusValidationResult
    {
        List<ValidationResult> Errors { get; }
    }
}