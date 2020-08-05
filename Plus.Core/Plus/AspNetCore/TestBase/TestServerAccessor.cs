#if NETCOREAPP3_1

using Microsoft.AspNetCore.TestHost;
using Plus.DependencyInjection;

namespace Plus.AspNetCore.TestBase
{
    public class TestServerAccessor : ITestServerAccessor, ISingletonDependency
    {
        public TestServer Server { get; set; }
    }
}

#endif