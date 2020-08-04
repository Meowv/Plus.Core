namespace Plus.Data
{
    public class PlusDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public PlusDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
        }
    }
}