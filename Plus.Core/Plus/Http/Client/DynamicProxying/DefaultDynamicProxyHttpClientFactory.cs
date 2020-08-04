using Plus.DependencyInjection;
using System.Net.Http;

namespace Plus.Http.Client.DynamicProxying
{
    public class DefaultDynamicProxyHttpClientFactory : IDynamicProxyHttpClientFactory, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultDynamicProxyHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient Create()
        {
            return _httpClientFactory.CreateClient();
        }

        public HttpClient Create(string name)
        {
            return _httpClientFactory.CreateClient(name);
        }
    }
}