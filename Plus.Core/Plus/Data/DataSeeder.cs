using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Plus.DependencyInjection;
using Plus.Uow;
using System.Threading.Tasks;

namespace Plus.Data
{
    //TODO: Create a Plus.Data.Seeding namespace?
    public class DataSeeder : IDataSeeder, ITransientDependency
    {
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }
        protected PlusDataSeedOptions Options { get; }

        public DataSeeder(
            IOptions<PlusDataSeedOptions> options,
            IHybridServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Options = options.Value;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (var scope = ServiceScopeFactory.CreateScope())
            {
                foreach (var contributorType in Options.Contributors)
                {
                    var contributor = (IDataSeedContributor)scope
                        .ServiceProvider
                        .GetRequiredService(contributorType);

                    await contributor.SeedAsync(context);
                }
            }
        }
    }
}