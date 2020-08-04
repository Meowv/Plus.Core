namespace Plus.Ldap.Modeling
{
    public interface ILdapOrganization : ILdapEntry
    {
        string OrganizationUnit { get; set; }
    }
}