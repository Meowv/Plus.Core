using Plus.Application.Services;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations
{
    public interface IPlusApplicationConfigurationAppService : IApplicationService
    {
        Task<ApplicationConfigurationDto> GetAsync();
    }
}