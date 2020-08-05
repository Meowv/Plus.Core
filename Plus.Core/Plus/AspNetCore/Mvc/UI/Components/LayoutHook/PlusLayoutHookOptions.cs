using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.UI.Components.LayoutHook
{
    public class PlusLayoutHookOptions
    {
        public IDictionary<string, List<LayoutHookInfo>> Hooks { get; }

        public PlusLayoutHookOptions()
        {
            Hooks = new Dictionary<string, List<LayoutHookInfo>>();
        }

        public PlusLayoutHookOptions Add(string name, Type componentType, string layout = null)
        {
            Hooks
                .GetOrAdd(name, () => new List<LayoutHookInfo>())
                .Add(new LayoutHookInfo(componentType, layout));

            return this;
        }
    }
}