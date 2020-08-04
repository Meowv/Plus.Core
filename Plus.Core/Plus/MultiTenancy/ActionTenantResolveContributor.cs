using JetBrains.Annotations;
using System;

namespace Plus.MultiTenancy
{
    public class ActionTenantResolveContributor : TenantResolveContributorBase
    {
        public const string ContributorName = "Action";

        public override string Name => ContributorName;

        private readonly Action<ITenantResolveContext> _resolveAction;

        public ActionTenantResolveContributor([NotNull] Action<ITenantResolveContext> resolveAction)
        {
            Check.NotNull(resolveAction, nameof(resolveAction));

            _resolveAction = resolveAction;
        }

        public override void Resolve(ITenantResolveContext context)
        {
            _resolveAction(context);
        }
    }
}