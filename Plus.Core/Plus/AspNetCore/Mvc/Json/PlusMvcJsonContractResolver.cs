using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plus.Json.Newtonsoft;
using Plus.Reflection;
using Plus.Timing;
using System;
using System.Reflection;

namespace Plus.AspNetCore.Mvc.Json
{
    public class PlusMvcJsonContractResolver : DefaultContractResolver
    {
        private readonly Lazy<PlusJsonIsoDateTimeConverter> _dateTimeConverter;

        public PlusMvcJsonContractResolver(IServiceCollection services)
        {
            _dateTimeConverter = services.GetServiceLazy<PlusJsonIsoDateTimeConverter>();

            NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                property.Converter = _dateTimeConverter.Value;
            }

        }
    }
}