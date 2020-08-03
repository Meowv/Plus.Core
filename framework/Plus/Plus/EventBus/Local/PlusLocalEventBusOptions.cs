using Plus.Collections;

namespace Plus.EventBus.Local
{
    public class PlusLocalEventBusOptions
    {
        public ITypeList<IEventHandler> Handlers { get; }

        public PlusLocalEventBusOptions()
        {
            Handlers = new TypeList<IEventHandler>();
        }
    }
}