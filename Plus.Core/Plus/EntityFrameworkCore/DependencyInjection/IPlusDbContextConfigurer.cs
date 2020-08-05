namespace Plus.EntityFrameworkCore.DependencyInjection
{
    public interface IPlusDbContextConfigurer
    {
        void Configure(PlusDbContextConfigurationContext context);
    }

    public interface IPlusDbContextConfigurer<TDbContext>
        where TDbContext : PlusDbContext<TDbContext>
    {
        void Configure(PlusDbContextConfigurationContext<TDbContext> context);
    }
}