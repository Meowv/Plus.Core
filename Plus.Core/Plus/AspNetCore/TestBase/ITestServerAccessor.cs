#if NETCOREAPP3_1

using Microsoft.AspNetCore.TestHost;

namespace Plus.AspNetCore.TestBase
{
    public interface ITestServerAccessor 
    {
        TestServer Server { get; set; }
    }
}

#endif