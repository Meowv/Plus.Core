using System.Threading.Tasks;
using Xunit;

namespace Plus.AspNetCore.MultiTenancy
{
    public class AspNetCoreMultiTenancy_Without_DomainResolver_Tests : AspNetCoreMultiTenancyTestBase
    {

        [Fact]
        public async Task Should_Use_Host_If_Tenant_Is_Not_Specified()
        {
            
        }

        [Fact]
        public async Task Should_Use_QueryString_Tenant_Id_If_Specified()
        {

        }

        [Fact]
        public async Task Should_Use_Header_Tenant_Id_If_Specified()
        {

        }

        [Fact]
        public async Task Should_Use_Cookie_Tenant_Id_If_Specified()
        {

        }
    }
}