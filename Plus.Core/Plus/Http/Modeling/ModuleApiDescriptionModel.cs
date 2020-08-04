using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plus.Http.Modeling
{
    [Serializable]
    public class ModuleApiDescriptionModel
    {
        /// <summary>
        /// "app".
        /// </summary>
        public const string DefaultRootPath = "app";

        /// <summary>
        /// "Default".
        /// </summary>
        public const string DefaultRemoteServiceName = "Default";

        public string RootPath { get; set; }

        public string RemoteServiceName { get; set; }

        public IDictionary<string, ControllerApiDescriptionModel> Controllers { get; set; }

        private ModuleApiDescriptionModel()
        {

        }

        public static ModuleApiDescriptionModel Create(string rootPath, string remoteServiceName)
        {
            return new ModuleApiDescriptionModel
            {
                RootPath = rootPath,
                RemoteServiceName = remoteServiceName,
                Controllers = new Dictionary<string, ControllerApiDescriptionModel>()
            };
        }

        public ControllerApiDescriptionModel AddController(ControllerApiDescriptionModel controller)
        {
            if (Controllers.ContainsKey(controller.ControllerName))
            {
                throw new PlusException($"There is already a controller with name: {controller.ControllerName} in module: {RootPath}");
            }

            return Controllers[controller.ControllerName] = controller;
        }

        public ControllerApiDescriptionModel GetOrAddController(string uniqueName, string name, Type type, [CanBeNull] HashSet<Type> ignoredInterfaces = null)
        {
            return Controllers.GetOrAdd(uniqueName, () => ControllerApiDescriptionModel.Create(name, type, ignoredInterfaces));
        }

        public ModuleApiDescriptionModel CreateSubModel(string[] controllers, string[] actions)
        {
            var subModel = Create(RootPath, RemoteServiceName);

            foreach (var controller in Controllers.Values)
            {
                if (controllers == null || controllers.Contains(controller.ControllerName))
                {
                    subModel.AddController(controller.CreateSubModel(actions));
                }
            }

            return subModel;
        }
    }
}