using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.UI.Navigation.Urls
{
    public interface IAppUrlProvider
    {
        Task<string> GetUrlAsync([NotNull] string appName, [CanBeNull] string urlName = null);
    }
}