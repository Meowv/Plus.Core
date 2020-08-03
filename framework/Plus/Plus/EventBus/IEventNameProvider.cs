using System;

namespace Plus.EventBus
{
    public interface IEventNameProvider
    {
        string GetName(Type eventType);
    }
}