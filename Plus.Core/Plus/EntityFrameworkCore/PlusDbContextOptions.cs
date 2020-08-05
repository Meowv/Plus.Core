using JetBrains.Annotations;
using Plus.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore
{
    public class PlusDbContextOptions
    {
        internal List<Action<PlusDbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        internal Action<PlusDbContextConfigurationContext> DefaultConfigureAction { get; set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        internal Dictionary<Type, object> ConfigureActions { get; set; }

        public PlusDbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<PlusDbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
        }

        public void PreConfigure([NotNull] Action<PlusDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultPreConfigureActions.Add(action);
        }

        public void Configure([NotNull] Action<PlusDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultConfigureAction = action;
        }

        public void PreConfigure<TDbContext>([NotNull] Action<PlusDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : PlusDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            var actions = PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (actions == null)
            {
                PreConfigureActions[typeof(TDbContext)] = actions = new List<object>();
            }

            actions.Add(action);
        }

        public void Configure<TDbContext>([NotNull] Action<PlusDbContextConfigurationContext<TDbContext>> action)
            where TDbContext : PlusDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }
    }
}