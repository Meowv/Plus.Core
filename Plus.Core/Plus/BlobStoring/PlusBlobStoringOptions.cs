namespace Plus.BlobStoring
{
    public class PlusBlobStoringOptions
    {
        public BlobContainerConfigurations Containers { get; }

        public PlusBlobStoringOptions()
        {
            Containers = new BlobContainerConfigurations();
        }
    }
}