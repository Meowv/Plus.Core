using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plus.Timing;
using System;

namespace Plus.EntityFrameworkCore.ValueConverters
{
    public class PlusDateTimeValueConverter : ValueConverter<DateTime?, DateTime?>
    {
        public PlusDateTimeValueConverter(IClock clock, [CanBeNull] ConverterMappingHints mappingHints = null)
            : base(
                x => x.HasValue ? clock.Normalize(x.Value) : x,
                x => x.HasValue ? clock.Normalize(x.Value) : x, mappingHints)
        {
        }
    }
}