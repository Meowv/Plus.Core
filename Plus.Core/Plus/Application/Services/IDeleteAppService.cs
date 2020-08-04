using System.Threading.Tasks;

namespace Plus.Application.Services
{
    public interface IDeleteAppService<in TKey> : IApplicationService
    {
        Task DeleteAsync(TKey id);
    }
}