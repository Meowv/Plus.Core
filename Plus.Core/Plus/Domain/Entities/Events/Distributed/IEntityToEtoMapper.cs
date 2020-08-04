using JetBrains.Annotations;

namespace Plus.Domain.Entities.Events.Distributed
{
    public interface IEntityToEtoMapper
    {
        [CanBeNull]
        object Map(object entityObj);
    }
}