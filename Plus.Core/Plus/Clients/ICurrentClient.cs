namespace Plus.Clients
{
    public interface ICurrentClient
    {
        string Id { get; }

        bool IsAuthenticated { get; }
    }
}