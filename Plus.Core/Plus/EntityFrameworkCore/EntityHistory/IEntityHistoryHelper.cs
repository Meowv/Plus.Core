using Microsoft.EntityFrameworkCore.ChangeTracking;
using Plus.Auditing;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore.EntityHistory
{
    public interface IEntityHistoryHelper
    {
        List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries);

        void UpdateChangeList(List<EntityChangeInfo> entityChanges);
    }
}