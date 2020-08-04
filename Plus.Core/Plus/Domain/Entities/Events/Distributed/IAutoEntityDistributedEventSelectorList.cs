using System.Collections.Generic;

namespace Plus.Domain.Entities.Events.Distributed
{
    public interface IAutoEntityDistributedEventSelectorList : IList<NamedTypeSelector>
    {
    }
}