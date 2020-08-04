using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Validation
{
    public class PlusValidationResult : IPlusValidationResult
    {
        public List<ValidationResult> Errors { get; }

        public PlusValidationResult()
        {
            Errors = new List<ValidationResult>();
        }
    }
}