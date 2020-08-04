using System;
using System.Net.Http;

namespace Plus.IdentityModel
{
    public class IdentityModelHttpRequestMessageOptions
    {
        public Action<HttpRequestMessage> ConfigureHttpRequestMessage { get; set; }
    }
}