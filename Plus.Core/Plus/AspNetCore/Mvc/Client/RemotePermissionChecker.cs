﻿using Plus.Authorization.Permissions;
using Plus.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.Client
{
    public class RemotePermissionChecker : IPermissionChecker, ITransientDependency
    {
        protected ICachedApplicationConfigurationClient ConfigurationClient { get; }

        public RemotePermissionChecker(ICachedApplicationConfigurationClient configurationClient)
        {
            ConfigurationClient = configurationClient;
        }

        public async Task<bool> IsGrantedAsync(string name)
        {
            var configuration = await ConfigurationClient.GetAsync();

            return configuration.Auth.GrantedPolicies.ContainsKey(name);
        }

        public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
        {
            /* This provider always works for the current principal. */
            return await IsGrantedAsync(name);
        }
    }
}