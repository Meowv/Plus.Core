using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plus.DependencyInjection;
using Plus.Timing;
using System;

namespace Plus.Json.Newtonsoft
{
    public class PlusJsonIsoDateTimeConverter : IsoDateTimeConverter, ITransientDependency
    {
        private readonly IClock _clock;

        public PlusJsonIsoDateTimeConverter(IClock clock, IOptions<PlusJsonOptions> PlusJsonOptions)
        {
            _clock = clock;

            if (PlusJsonOptions.Value.DefaultDateTimeFormat != null)
            {
                DateTimeFormat = PlusJsonOptions.Value.DefaultDateTimeFormat;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue)
            {
                return _clock.Normalize(date.Value);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            base.WriteJson(writer, date.HasValue ? _clock.Normalize(date.Value) : value, serializer);
        }
    }
}