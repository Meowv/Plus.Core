#if NETCOREAPP3_1

namespace Plus.AspNetCore.Mvc.AntiForgery
{
    public static class PlusAntiForgeryManagerAspNetCoreExtensions
    {
        public static void SetCookie(this IPlusAntiForgeryManager manager)
        {
            manager.HttpContext.Response.Cookies.Append(manager.Options.TokenCookieName, manager.GenerateToken());
        }
    }
}

#endif