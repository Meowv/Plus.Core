using Plus.AspNetCore.Mvc.ApplicationConfigurations;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Client
{
    public interface ICachedApplicationConfigurationClient
    {
        Task<ApplicationConfigurationDto> GetAsync();
    }
}