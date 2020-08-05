using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    [Serializable]
    public class ObjectExtensionsDto
    {
        public Dictionary<string, ModuleExtensionDto> Modules { get; set; }

        public Dictionary<string, ExtensionEnumDto> Enums { get; set; }
    }
}