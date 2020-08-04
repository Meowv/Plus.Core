using Plus.EventBus;
using System;

namespace Plus.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Created")]
    public class EntityCreatedEto<TEntityEto>
    {
        public TEntityEto Entity { get; set; }

        public EntityCreatedEto(TEntityEto entity)
        {
            Entity = entity;
        }
    }
}