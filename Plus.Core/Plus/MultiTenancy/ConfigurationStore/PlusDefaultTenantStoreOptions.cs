namespace Plus.MultiTenancy.ConfigurationStore
{
    public class PlusDefaultTenantStoreOptions
    {
        public TenantConfiguration[] Tenants { get; set; }

        public PlusDefaultTenantStoreOptions()
        {
            Tenants = new TenantConfiguration[0];
        }
    }
}