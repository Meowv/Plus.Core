using JetBrains.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.Validation
{
    public class ObjectValidationContext
    {
        [NotNull]
        public object ValidatingObject { get; }

        public List<ValidationResult> Errors { get; }

        public ObjectValidationContext([NotNull] object validatingObject)
        {
            ValidatingObject = Check.NotNull(validatingObject, nameof(validatingObject));
            Errors = new List<ValidationResult>();
        }
    }
}