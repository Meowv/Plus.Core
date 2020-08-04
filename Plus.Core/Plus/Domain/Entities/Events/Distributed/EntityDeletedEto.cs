using Plus.EventBus;
using System;

namespace Plus.Domain.Entities.Events.Distributed
{
    [Serializable]
    [GenericEventName(Postfix = ".Deleted")]
    public class EntityDeletedEto<TEntityEto>
    {
        public TEntityEto Entity { get; set; }

        public EntityDeletedEto(TEntityEto entity)
        {
            Entity = entity;
        }
    }
}