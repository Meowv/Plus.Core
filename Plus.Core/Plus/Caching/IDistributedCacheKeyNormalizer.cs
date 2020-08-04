namespace Plus.Caching
{
    public interface IDistributedCacheKeyNormalizer
    {
        string NormalizeKey(DistributedCacheKeyNormalizeArgs args);
    }
}