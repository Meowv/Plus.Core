using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;

namespace Plus.Caching
{
    public class PlusDistributedCacheOptions
    {
        /// <summary>
        /// Throw or hide exceptions for the distributed cache.
        /// </summary>
        public bool HideErrors { get; set; } = true;

        /// <summary>
        /// Cache key prefix.
        /// </summary>
        public string KeyPrefix { get; set; }

        /// <summary>
        /// Global Cache entry options.
        /// </summary>
        public DistributedCacheEntryOptions GlobalCacheEntryOptions { get; set; }

        /// <summary>
        /// List of all cache configurators.
        /// (func argument:Name of cache)
        /// </summary>
        public List<Func<string, DistributedCacheEntryOptions>> CacheConfigurators { get; set; } //TODO: use a configurator interface instead?

        public PlusDistributedCacheOptions()
        {
            CacheConfigurators = new List<Func<string, DistributedCacheEntryOptions>>();
            GlobalCacheEntryOptions = new DistributedCacheEntryOptions();
            KeyPrefix = "";
        }
    }
}