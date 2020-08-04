namespace Plus.Ldap
{
    public class PlusLdapOptions
    {
        public string ServerHost { get; set; }

        public int ServerPort { get; set; }

        public bool UseSsl { get; set; }

        public string SearchBase { get; set; }

        public string DomainName { get; set; }

        public string DomainDistinguishedName { get; set; }

        public LdapCredentials Credentials { get; set; }

        public PlusLdapOptions()
        {
            Credentials = new LdapCredentials();
        }
    }
}