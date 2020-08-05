namespace Plus.EntityFrameworkCore
{
    public interface IPlusEfCoreDbContext : IEfCoreDbContext
    {
        void Initialize(PlusEfCoreDbContextInitializationContext initializationContext);
    }
}