using System.Net.Http;

namespace Plus.Http.Client.DynamicProxying
{
    public interface IDynamicProxyHttpClientFactory
    {
        HttpClient Create();

        HttpClient Create(string name);
    }
}