using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Authorization
{
    public interface IPlusAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        Task<List<string>> GetPoliciesNamesAsync();
    }
}