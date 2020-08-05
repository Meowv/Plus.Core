using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Plus.EntityFrameworkCore.ValueConverters
{
    public class PlusJsonValueConverter<TPropertyType> : ValueConverter<TPropertyType, string>
    {
        public PlusJsonValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(TPropertyType d)
        {
            return JsonConvert.SerializeObject(d, Formatting.None);
        }

        private static TPropertyType DeserializeObject(string s)
        {
            return JsonConvert.DeserializeObject<TPropertyType>(s);
        }
    }
}