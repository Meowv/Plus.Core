using System.Threading.Tasks;

namespace Plus.Data
{
    public interface IDataSeeder
    {
        Task SeedAsync(DataSeedContext context);
    }
}