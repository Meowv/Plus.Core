#if NETCOREAPP3_1

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Plus.AspNetCore.Mvc.UI.Components.LayoutHook
{
    public static class ViewComponentHelperLayoutHookExtensions
    {
        public static Task<IHtmlContent> InvokeLayoutHookAsync(
            this IViewComponentHelper componentHelper,
            string name,
            string layout)
        {
            return componentHelper.InvokeAsync(
                typeof(LayoutHookViewComponent),
                new
                {
                    name = name,
                    layout = layout
                }
            );
        }
    }
}

#endif