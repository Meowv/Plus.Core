#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Plus.Users;

namespace Plus.AspNetCore.Mvc.UI.RazorPages
{
    public abstract class PlusPage : Page
    {
        [RazorInject]
        public ICurrentUser CurrentUser { get; set; }
    }
}

#endif