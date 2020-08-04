using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.MultiTenancy;

namespace Plus.Caching
{
    public class DistributedCacheKeyNormalizer : IDistributedCacheKeyNormalizer, ITransientDependency
    {
        protected ICurrentTenant CurrentTenant { get; }

        protected PlusDistributedCacheOptions DistributedCacheOptions { get; }

        public DistributedCacheKeyNormalizer(
            ICurrentTenant currentTenant,
            IOptions<PlusDistributedCacheOptions> distributedCacheOptions)
        {
            CurrentTenant = currentTenant;
            DistributedCacheOptions = distributedCacheOptions.Value;
        }

        public virtual string NormalizeKey(DistributedCacheKeyNormalizeArgs args)
        {
            var normalizedKey = $"c:{args.CacheName},k:{DistributedCacheOptions.KeyPrefix}{args.Key}";

            if (!args.IgnoreMultiTenancy && CurrentTenant.Id.HasValue)
            {
                normalizedKey = $"t:{CurrentTenant.Id.Value},{normalizedKey}";
            }

            return normalizedKey;
        }
    }
}