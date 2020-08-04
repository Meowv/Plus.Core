namespace Plus.MongoDB
{
    public interface IMongoModelSource
    {
        MongoDbContextModel GetModel(PlusMongoDbContext dbContext);
    }
}