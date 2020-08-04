using MongoDB.Bson.Serialization;

namespace Plus.MongoDB
{
    public interface IHasBsonClassMap
    {
        BsonClassMap GetMap();
    }
}