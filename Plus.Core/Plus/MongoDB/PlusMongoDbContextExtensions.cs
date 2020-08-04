namespace Plus.MongoDB
{
    public static class PlusMongoDbContextExtensions
    {
        public static PlusMongoDbContext ToPlusMongoDbContext(this IPlusMongoDbContext dbContext)
        {
            var PlusMongoDbContext = dbContext as PlusMongoDbContext;

            if (PlusMongoDbContext == null)
            {
                throw new PlusException($"The type {dbContext.GetType().AssemblyQualifiedName} should be convertable to {typeof(PlusMongoDbContext).AssemblyQualifiedName}!");
            }

            return PlusMongoDbContext;
        }
    }
}