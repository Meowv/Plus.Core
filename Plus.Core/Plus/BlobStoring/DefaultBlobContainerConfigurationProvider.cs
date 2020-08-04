using Microsoft.Extensions.Options;
using Plus.DependencyInjection;

namespace Plus.BlobStoring
{
    public class DefaultBlobContainerConfigurationProvider : IBlobContainerConfigurationProvider, ITransientDependency
    {
        protected PlusBlobStoringOptions Options { get; }

        public DefaultBlobContainerConfigurationProvider(IOptions<PlusBlobStoringOptions> options)
        {
            Options = options.Value;
        }

        public virtual BlobContainerConfiguration Get(string name)
        {
            return Options.Containers.GetConfiguration(name);
        }
    }
}