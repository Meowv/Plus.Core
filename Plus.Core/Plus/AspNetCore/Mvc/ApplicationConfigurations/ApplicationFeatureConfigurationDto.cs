using System;
using System.Collections.Generic;

namespace Plus.AspNetCore.Mvc.ApplicationConfigurations
{
    [Serializable]
    public class ApplicationFeatureConfigurationDto
    {
        public Dictionary<string, string> Values { get; set; }

        public ApplicationFeatureConfigurationDto()
        {
            Values = new Dictionary<string, string>();
        }
    }
}