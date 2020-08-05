#if NETCOREAPP3_1

using Plus.DependencyInjection;
using Plus.Http.Client.DynamicProxying;
using System.Net.Http;

namespace Plus.AspNetCore.TestBase.DynamicProxying
{
    [Dependency(ReplaceServices = true)]
    public class AspNetCoreTestDynamicProxyHttpClientFactory : IDynamicProxyHttpClientFactory, ITransientDependency
    {
        private readonly ITestServerAccessor _testServerAccessor;

        public AspNetCoreTestDynamicProxyHttpClientFactory(
            ITestServerAccessor testServerAccessor)
        {
            _testServerAccessor = testServerAccessor;
        }

        public HttpClient Create()
        {
            return _testServerAccessor.Server.CreateClient();
        }

        public HttpClient Create(string name)
        {
            return Create();
        }
    }
}

#endif