using Plus.Collections;

namespace Plus.EventBus.Distributed
{
    public class PlusDistributedEventBusOptions
    {
        public ITypeList<IEventHandler> Handlers { get; }

        public PlusDistributedEventBusOptions()
        {
            Handlers = new TypeList<IEventHandler>();
        }
    }
}