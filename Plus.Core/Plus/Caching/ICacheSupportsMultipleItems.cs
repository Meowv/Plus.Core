using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plus.Caching
{
    public interface ICacheSupportsMultipleItems
    {
        byte[][] GetMany(
            IEnumerable<string> keys
        );

        Task<byte[][]> GetManyAsync(
            IEnumerable<string> keys,
            CancellationToken token = default
        );

        void SetMany(
            IEnumerable<KeyValuePair<string, byte[]>> items,
            DistributedCacheEntryOptions options
        );

        Task SetManyAsync(
            IEnumerable<KeyValuePair<string, byte[]>> items,
            DistributedCacheEntryOptions options,
            CancellationToken token = default
        );
    }
}