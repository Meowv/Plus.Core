#if NETCOREAPP3_1

using Microsoft.AspNetCore.Http;

namespace Plus.AspNetCore.Mvc.AntiForgery
{
    public interface IPlusAntiForgeryManager
    {
        PlusAntiForgeryOptions Options { get; }

        HttpContext HttpContext { get; }

        void SetCookie();

        string GenerateToken();
    }
}

#endif