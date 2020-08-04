using Plus.EventBus;
using System;

namespace Plus.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Updated")]
    public class EntityUpdatedEto<TEntityEto>
    {
        public TEntityEto Entity { get; set; }

        public EntityUpdatedEto(TEntityEto entity)
        {
            Entity = entity;
        }
    }
}