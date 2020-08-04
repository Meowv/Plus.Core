using MongoDB.Driver;

namespace Plus.MongoDB
{
    public interface IPlusMongoDbContext
    {
        IMongoDatabase Database { get; }

        IMongoCollection<T> Collection<T>();
    }
}