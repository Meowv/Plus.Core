using Novell.Directory.Ldap;

namespace Plus.Ldap.Modeling
{
    public class LdapOrganization : LdapEntryBase, ILdapOrganization
    {
        public string OrganizationUnit { get; set; }

        public LdapOrganization() { }

        public LdapOrganization(LdapAttributeSet attributeSet)
            : base(attributeSet)
        {
            OrganizationUnit = attributeSet.getAttribute("ou")?.StringValue;
        }
    }
}