using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Plus.DependencyInjection;

namespace Plus.Auditing
{
    public class JsonNetAuditSerializer : IAuditSerializer, ITransientDependency
    {
        protected PlusAuditingOptions Options;

        public JsonNetAuditSerializer(IOptions<PlusAuditingOptions> options)
        {
            Options = options.Value;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, GetSharedJsonSerializerSettings());
        }

        private static readonly object SyncObj = new object();
        private static JsonSerializerSettings _sharedJsonSerializerSettings;

        private JsonSerializerSettings GetSharedJsonSerializerSettings()
        {
            if (_sharedJsonSerializerSettings == null)
            {
                lock (SyncObj)
                {
                    if (_sharedJsonSerializerSettings == null)
                    {
                        _sharedJsonSerializerSettings = new JsonSerializerSettings
                        {
                            ContractResolver = new AuditingContractResolver(Options.IgnoredTypes)
                        };
                    }
                }
            }

            return _sharedJsonSerializerSettings;
        }
    }
}