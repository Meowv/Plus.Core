using RabbitMQ.Client;
using System;

namespace Plus.RabbitMQ
{
    public interface IConnectionPool : IDisposable
    {
        IConnection Get(string connectionName = null);
    }
}