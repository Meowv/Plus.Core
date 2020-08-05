#if NETCOREAPP3_1

using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Plus.Http.Modeling;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Plus.AspNetCore.Mvc.Conventions
{
    public class PlusConventionalControllerOptions
    {
        public ConventionalControllerSettingList ConventionalControllerSettings { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public PlusConventionalControllerOptions()
        {
            ConventionalControllerSettings = new ConventionalControllerSettingList();

            FormBodyBindingIgnoredTypes = new List<Type>
            {
                typeof(IFormFile)
            };
        }

        public PlusConventionalControllerOptions Create(
            Assembly assembly,
            [CanBeNull] Action<ConventionalControllerSetting> optionsAction = null)
        {
            var setting = new ConventionalControllerSetting(
                assembly,
                ModuleApiDescriptionModel.DefaultRootPath,
                ModuleApiDescriptionModel.DefaultRemoteServiceName
            );

            optionsAction?.Invoke(setting);
            setting.Initialize();
            ConventionalControllerSettings.Add(setting);
            return this;
        }
    }
}

#endif