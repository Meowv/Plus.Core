using JetBrains.Annotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Plus.Authorization.Permissions
{
    public interface IPermissionChecker
    {
        Task<bool> IsGrantedAsync([NotNull] string name);

        Task<bool> IsGrantedAsync([CanBeNull] ClaimsPrincipal claimsPrincipal, [NotNull] string name);
    }
}