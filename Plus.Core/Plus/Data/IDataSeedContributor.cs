using System.Threading.Tasks;

namespace Plus.Data
{
    public interface IDataSeedContributor
    {
        Task SeedAsync(DataSeedContext context);
    }
}