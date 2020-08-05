#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Plus.Localization;
using System;

namespace Plus.AspNetCore.Mvc.Localization
{
    [Area("Plus")]
    [Route("Plus/Languages/[action]")]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PlusLanguagesController : PlusController
    {
        [HttpGet]
        public IActionResult Switch(string culture, string uiCulture = "", string returnUrl = "")
        {
            if (!CultureHelper.IsValidCultureCode(culture))
            {
                throw new PlusException("Unknown language: " + culture + ". It must be a valid culture!");
            }

            string cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, uiCulture));

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue, new CookieOptions
            {
                Expires = Clock.Now.AddYears(2)
            });

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(GetRedirectUrl(returnUrl));
            }

            return Redirect("~/");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                return "~/";
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return "~/";
        }
    }
}

#endif