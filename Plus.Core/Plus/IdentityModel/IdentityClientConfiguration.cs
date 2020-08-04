﻿using IdentityModel;
using System;
using System.Collections.Generic;

namespace Plus.IdentityModel
{
    public class IdentityClientConfiguration : Dictionary<string, string>
    {
        /// <summary>
        /// Possible values: "client_credentials" or "password".
        /// Default value: "client_credentials".
        /// </summary>
        public string GrantType
        {
            get => this.GetOrDefault(nameof(GrantType));
            set => this[nameof(GrantType)] = value;
        }

        /// <summary>
        /// Client Id.
        /// </summary>
        public string ClientId
        {
            get => this.GetOrDefault(nameof(ClientId));
            set => this[nameof(ClientId)] = value;
        }

        /// <summary>
        /// Client secret (as plain text - without hashed).
        /// </summary>
        public string ClientSecret
        {
            get => this.GetOrDefault(nameof(ClientSecret));
            set => this[nameof(ClientSecret)] = value;
        }

        /// <summary>
        /// User name.
        /// Valid only if <see cref="GrantType"/> is "password".
        /// </summary>
        public string UserName
        {
            get => this.GetOrDefault(nameof(UserName));
            set => this[nameof(UserName)] = value;
        }

        /// <summary>
        /// Password of the <see cref="UserName"/>.
        /// Valid only if <see cref="GrantType"/> is "password".
        /// </summary>
        public string UserPassword
        {
            get => this.GetOrDefault(nameof(UserPassword));
            set => this[nameof(UserPassword)] = value;
        }

        /// <summary>
        /// Authority.
        /// </summary>
        public string Authority
        {
            get => this.GetOrDefault(nameof(Authority));
            set => this[nameof(Authority)] = value;
        }

        /// <summary>
        /// Scope.
        /// </summary>
        public string Scope
        {
            get => this.GetOrDefault(nameof(Scope));
            set => this[nameof(Scope)] = value;
        }

        /// <summary>
        /// RequireHttps.
        /// Default: true.
        /// </summary>
        public bool RequireHttps
        {
            get => this.GetOrDefault(nameof(RequireHttps))?.To<bool>() ?? true;
            set => this[nameof(RequireHttps)] = value.ToString().ToLowerInvariant();
        }

        public IdentityClientConfiguration()
        {

        }

        public IdentityClientConfiguration(
            string authority,
            string scope,
            string clientId,
            string clientSecret,
            string grantType = OidcConstants.GrantTypes.ClientCredentials,
            string userName = null,
            string userPassword = null,
            bool requireHttps = true)
        {
            this[nameof(Authority)] = authority;
            this[nameof(Scope)] = scope;
            this[nameof(ClientId)] = clientId;
            this[nameof(ClientSecret)] = clientSecret;
            this[nameof(GrantType)] = grantType;
            this[nameof(UserName)] = userName;
            this[nameof(UserPassword)] = userPassword;
            this[nameof(RequireHttps)] = requireHttps.ToString().ToLowerInvariant();
        }
    }
}