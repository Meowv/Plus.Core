using Plus.Application.Services;
using System;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.MultiTenancy
{
    public interface IPlusTenantAppService : IApplicationService
    {
        Task<FindTenantResultDto> FindTenantByNameAsync(string name);

        Task<FindTenantResultDto> FindTenantByIdAsync(Guid id);
    }
}