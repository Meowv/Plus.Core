using Microsoft.EntityFrameworkCore.ChangeTracking;
using Plus.Auditing;
using System.Collections.Generic;

namespace Plus.EntityFrameworkCore.EntityHistory
{
    public class NullEntityHistoryHelper : IEntityHistoryHelper
    {
        public static NullEntityHistoryHelper Instance { get; } = new NullEntityHistoryHelper();

        private NullEntityHistoryHelper()
        {

        }

        public List<EntityChangeInfo> CreateChangeList(ICollection<EntityEntry> entityEntries)
        {
            return new List<EntityChangeInfo>();
        }

        public void UpdateChangeList(List<EntityChangeInfo> entityChanges)
        {

        }
    }
}