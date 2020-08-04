namespace Plus.Http.Client
{
    public class PlusRemoteServiceOptions
    {
        public RemoteServiceConfigurationDictionary RemoteServices { get; set; }

        public PlusRemoteServiceOptions()
        {
            RemoteServices = new RemoteServiceConfigurationDictionary();
        }
    }
}