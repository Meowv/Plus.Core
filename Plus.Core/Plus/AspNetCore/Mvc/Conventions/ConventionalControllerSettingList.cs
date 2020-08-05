#if NETCOREAPP3_1

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.AspNetCore.Mvc.Conventions
{
    public class ConventionalControllerSettingList : List<ConventionalControllerSetting>
    {
        [CanBeNull]
        public ConventionalControllerSetting GetSettingOrNull(Type controllerType)
        {
            return this.FirstOrDefault(controllerSetting => controllerSetting.ControllerTypes.Contains(controllerType));
        }
    }
}

#endif