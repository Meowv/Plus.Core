#if NETCOREAPP3_1

using Plus.AspNetCore.Authentication.OAuth.Claims;
using Plus.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth.Claims
{
    public static class PlusClaimActionCollectionExtensions
    {
        public static void MapPlusClaimTypes(this ClaimActionCollection claimActions)
        {
            if (PlusClaimTypes.UserName != "name")
            {
                claimActions.MapJsonKey(PlusClaimTypes.UserName, "name");
                claimActions.DeleteClaim("name");
            }

            if (PlusClaimTypes.Email != "email")
            {
                claimActions.MapJsonKey(PlusClaimTypes.Email, "email");
                claimActions.DeleteClaim("email");
            }

            if (PlusClaimTypes.EmailVerified != "email_verified")
            {
                claimActions.MapJsonKey(PlusClaimTypes.EmailVerified, "email_verified");
            }

            if (PlusClaimTypes.PhoneNumber != "phone_number")
            {
                claimActions.MapJsonKey(PlusClaimTypes.PhoneNumber, "phone_number");
            }

            if (PlusClaimTypes.PhoneNumberVerified != "phone_number_verified")
            {
                claimActions.MapJsonKey(PlusClaimTypes.PhoneNumberVerified, "phone_number_verified");
            }

            if (PlusClaimTypes.Role != "role")
            {
                claimActions.MapJsonKeyMultiple(PlusClaimTypes.Role, "role");
            }
        }

        public static void MapJsonKeyMultiple(this ClaimActionCollection claimActions, string claimType, string jsonKey)
        {
            claimActions.Add(new MultipleClaimAction(claimType, jsonKey));
        }
    }
}

#endif