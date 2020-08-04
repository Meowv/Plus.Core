using System;
using System.Collections.Generic;

namespace Plus.Localization.ExceptionHandling
{
    public class PlusExceptionLocalizationOptions
    {
        public Dictionary<string, Type> ErrorCodeNamespaceMappings { get; }

        public PlusExceptionLocalizationOptions()
        {
            ErrorCodeNamespaceMappings = new Dictionary<string, Type>();
        }

        public void MapCodeNamespace(string errorCodeNamespace, Type type)
        {
            ErrorCodeNamespaceMappings[errorCodeNamespace] = type;
        }
    }
}