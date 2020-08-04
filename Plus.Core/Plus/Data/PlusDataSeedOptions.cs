namespace Plus.Data
{
    public class PlusDataSeedOptions
    {
        public DataSeedContributorList Contributors { get; }

        public PlusDataSeedOptions()
        {
            Contributors = new DataSeedContributorList();
        }
    }
}