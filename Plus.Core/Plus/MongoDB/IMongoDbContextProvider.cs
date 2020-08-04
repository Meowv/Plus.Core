namespace Plus.MongoDB
{
    public interface IMongoDbContextProvider<out TMongoDbContext>
        where TMongoDbContext : IPlusMongoDbContext
    {
        TMongoDbContext GetDbContext();
    }
}