using Plus.Security.Claims;
using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Security.Claims
{
    public class PlusClaimsMapOptions
    {
        public Dictionary<string, Func<string>> Maps { get; }

        public PlusClaimsMapOptions()
        {
            Maps = new Dictionary<string, Func<string>>()
            {
                { "sub", () => PlusClaimTypes.UserId },
                { "role", () => PlusClaimTypes.Role },
                { "email", () => PlusClaimTypes.Email },
            };
        }
    }
}