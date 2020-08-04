using JetBrains.Annotations;
using System.Collections.Generic;

namespace Plus.Validation.StringValues
{
    public interface IValueValidator
    {
        string Name { get; }

        [CanBeNull]
        object this[string key] { get; set; }

        [NotNull]
        IDictionary<string, object> Properties { get; }

        bool IsValid(object value);
    }
}