using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Plus.AspNetCore.Mvc.Localization
{
    public class PlusMvcDataAnnotationsLocalizationOptions
    {
        public IDictionary<Assembly, Type> AssemblyResources { get; }

        public PlusMvcDataAnnotationsLocalizationOptions()
        {
            AssemblyResources = new Dictionary<Assembly, Type>();
        }

        public void AddAssemblyResource(
            [NotNull] Type resourceType,
            params Assembly[] assemblies)
        {
            if (assemblies.IsNullOrEmpty())
            {
                assemblies = new[] { resourceType.Assembly };
            }

            foreach (var assembly in assemblies)
            {
                AssemblyResources[assembly] = resourceType;
            }
        }
    }
}