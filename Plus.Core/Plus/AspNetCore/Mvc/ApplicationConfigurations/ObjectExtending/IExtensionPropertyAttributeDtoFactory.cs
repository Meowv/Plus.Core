using System;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    public interface IExtensionPropertyAttributeDtoFactory
    {
        ExtensionPropertyAttributeDto Create(Attribute attribute);
    }
}