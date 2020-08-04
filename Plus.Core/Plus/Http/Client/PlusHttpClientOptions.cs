using Plus.Http.Client.DynamicProxying;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Plus.Http.Client
{
    public class PlusHttpClientOptions
    {
        public Dictionary<Type, DynamicHttpClientProxyConfig> HttpClientProxies { get; set; }

        public List<Func<string, Action<HttpClient>>> HttpClientActions { get; }

        public PlusHttpClientOptions()
        {
            HttpClientProxies = new Dictionary<Type, DynamicHttpClientProxyConfig>();
            HttpClientActions = new List<Func<string, Action<HttpClient>>>();
        }
    }
}