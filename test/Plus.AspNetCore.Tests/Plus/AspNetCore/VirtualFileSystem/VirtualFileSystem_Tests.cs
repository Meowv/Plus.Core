using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Plus.AspNetCore.VirtualFileSystem
{
    public class VirtualFileSystem_Tests : PlusAspNetCoreTestBase
    {
        [Fact]
        public async Task Get_Virtual_File()
        {
            var result = await GetResponseAsStringAsync(
                "/SampleFiles/test1.js"
            );

            result.ShouldBe("test1.js-content");
        }
    }
}