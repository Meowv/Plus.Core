#if NETCOREAPP3_1

using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Security.Claims;
using System.Text.Json;

namespace Plus.AspNetCore.Authentication.OAuth.Claims
{
    public class MultipleClaimAction : ClaimAction
    {
        public MultipleClaimAction(string claimType, string jsonKey)
            : base(claimType, jsonKey)
        {

        }

        public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
        {

            if (!userData.TryGetProperty(ValueType, out JsonElement prop))
                return;

            if (prop.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            switch (prop.ValueKind)
            {
                case JsonValueKind.String:
                    identity.AddClaim(new Claim(ClaimType, prop.GetString(), ValueType, issuer));
                    break;
                case JsonValueKind.Array:
                    foreach (var arramItem in prop.EnumerateArray())
                    {
                        identity.AddClaim(new Claim(ClaimType, arramItem.GetString(), ValueType, issuer));
                    }
                    break;
                default:
                    throw new PlusException("Unhandled JsonValueKind: " + prop.ValueKind);
            }
        }
    }
}

#endif