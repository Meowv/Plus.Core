namespace Plus.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}