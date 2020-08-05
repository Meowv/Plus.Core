using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending
{
    [Serializable]
    public class EntityExtensionDto
    {
        public Dictionary<string, ExtensionPropertyDto> Properties { get; set; }

        public Dictionary<string, object> Configuration { get; set; }
    }
}