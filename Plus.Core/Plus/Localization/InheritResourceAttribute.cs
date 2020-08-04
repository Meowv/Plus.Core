using System;

namespace Plus.Localization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InheritResourceAttribute : Attribute, IInheritedResourceTypesProvider
    {
        public Type[] ResourceTypes { get; }

        public InheritResourceAttribute(params Type[] resourceTypes)
        {
            ResourceTypes = resourceTypes ?? new Type[0];
        }

        public virtual Type[] GetInheritedResourceTypes()
        {
            return ResourceTypes;
        }
    }
}