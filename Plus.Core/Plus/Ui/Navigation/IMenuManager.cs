using System.Threading.Tasks;

namespace Plus.UI.Navigation
{
    public interface IMenuManager
    {
        Task<ApplicationMenu> GetAsync(string name);
    }
}