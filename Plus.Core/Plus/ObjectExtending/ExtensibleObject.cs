using Plus.Data;
using Plus.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plus.ObjectExtending
{
    [Serializable]
    public class ExtensibleObject : IHasExtraProperties, IValidatableObject
    {
        public Dictionary<string, object> ExtraProperties { get; protected set; }

        public ExtensibleObject()
            : this(true)
        {

        }

        public ExtensibleObject(bool setDefaultsForExtraProperties)
        {
            ExtraProperties = new Dictionary<string, object>();

            if (setDefaultsForExtraProperties)
            {
                this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return ExtensibleObjectValidator.GetValidationErrors(
                this,
                validationContext
            );
        }
    }
}