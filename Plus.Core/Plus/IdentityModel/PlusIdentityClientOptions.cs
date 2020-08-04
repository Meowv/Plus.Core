namespace Plus.IdentityModel
{
    public class PlusIdentityClientOptions
    {
        public IdentityClientConfigurationDictionary IdentityClients { get; set; }

        public PlusIdentityClientOptions()
        {
            IdentityClients = new IdentityClientConfigurationDictionary();
        }
    }
}