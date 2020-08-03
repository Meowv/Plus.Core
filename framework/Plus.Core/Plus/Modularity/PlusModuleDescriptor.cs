using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace Plus.Modularity
{
    public class PlusModuleDescriptor : IPlusModuleDescriptor
    {
        public Type Type { get; }

        public Assembly Assembly { get; }

        public IPlusModule Instance { get; }

        public bool IsLoadedAsPlugIn { get; }

        public IReadOnlyList<IPlusModuleDescriptor> Dependencies => _dependencies.ToImmutableList();
        private readonly List<IPlusModuleDescriptor> _dependencies;

        public PlusModuleDescriptor(
            [NotNull] Type type,
            [NotNull] IPlusModule instance,
            bool isLoadedAsPlugIn)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            if (!type.GetTypeInfo().IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");
            }

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugIn;

            _dependencies = new List<IPlusModuleDescriptor>();
        }

        public void AddDependency(IPlusModuleDescriptor descriptor)
        {
            _dependencies.AddIfNotContains(descriptor);
        }

        public override string ToString()
        {
            return $"[PlusModuleDescriptor {Type.FullName}]";
        }
    }
}
