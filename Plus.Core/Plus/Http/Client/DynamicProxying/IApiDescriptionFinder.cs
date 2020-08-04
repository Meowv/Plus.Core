using Plus.Http.Modeling;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Plus.Http.Client.DynamicProxying
{
    public interface IApiDescriptionFinder
    {
        Task<ActionApiDescriptionModel> FindActionAsync(HttpClient client, string baseUrl, Type serviceType, MethodInfo invocationMethod);

        Task<ApplicationApiDescriptionModel> GetApiDescriptionAsync(HttpClient client, string baseUrl);
    }
}