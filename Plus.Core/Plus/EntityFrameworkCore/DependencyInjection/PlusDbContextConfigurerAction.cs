using JetBrains.Annotations;
using System;

namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public class PlusDbContextConfigurerAction : IPlusDbContextConfigurer
    {
        [NotNull]
        public Action<PlusDbContextConfigurationContext> Action { get; }

        public PlusDbContextConfigurerAction([NotNull] Action<PlusDbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            Action = action;
        }

        public void Configure(PlusDbContextConfigurationContext context)
        {
            Action.Invoke(context);
        }
    }

    public class PlusDbContextConfigurerAction<TDbContext> : PlusDbContextConfigurerAction
        where TDbContext : PlusDbContext<TDbContext>
    {
        public PlusDbContextConfigurerAction([NotNull] Action<PlusDbContextConfigurationContext> action)
            : base(action)
        {
        }
    }
}