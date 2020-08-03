namespace Plus.RabbitMQ
{
    public class PlusRabbitMqOptions
    {
        public RabbitMqConnections Connections { get; }

        public PlusRabbitMqOptions()
        {
            Connections = new RabbitMqConnections();
        }
    }
}
