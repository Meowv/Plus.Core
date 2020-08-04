using Plus.Http.Modeling;
using System;
using System.Threading.Tasks;

namespace Plus.Http.Client.DynamicProxying
{
    public interface IApiDescriptionCache
    {
        Task<ApplicationApiDescriptionModel> GetAsync(
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory
        );
    }
}