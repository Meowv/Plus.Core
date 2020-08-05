#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.UI.Components.LayoutHook
{
    public class LayoutHookViewComponent : PlusViewComponent
    {
        protected PlusLayoutHookOptions Options { get; }

        public LayoutHookViewComponent(IOptions<PlusLayoutHookOptions> options)
        {
            Options = options.Value;
        }

        public virtual IViewComponentResult Invoke(string name, string layout)
        {
            var hooks = Options.Hooks.GetOrDefault(name)?.ToArray() ?? Array.Empty<LayoutHookInfo>();

            return View(
                "~/Plus/AspNetCore/Mvc/UI/Components/LayoutHook/Default.cshtml",
                new LayoutHookViewModel(hooks, layout)
            );
        }
    }
}

#endif