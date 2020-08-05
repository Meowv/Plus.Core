#if NETCOREAPP3_1

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlusMvcBuilderExtensions
    {
        public static void AddApplicationPartIfNotExists(this IMvcBuilder mvcBuilder, Assembly assembly)
        {
            mvcBuilder.PartManager.ApplicationParts.AddIfNotContains(assembly);
        }

        public static void AddApplicationPartIfNotExists(this IMvcCoreBuilder mvcCoreBuilder, Assembly assembly)
        {
            mvcCoreBuilder.PartManager.ApplicationParts.AddIfNotContains(assembly);
        }

        public static void AddIfNotContains(this IList<ApplicationPart> applicationParts, Assembly assembly)
        {
            if (applicationParts.Any(
                p => p is AssemblyPart assemblyPart && assemblyPart.Assembly == assembly))
            {
                return;
            }

            applicationParts.Add(new AssemblyPart(assembly));
        }
    }
}

#endif