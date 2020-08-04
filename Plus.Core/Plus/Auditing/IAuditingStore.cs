using System.Threading.Tasks;

namespace Plus.Auditing
{
    public interface IAuditingStore
    {
        Task SaveAsync(AuditLogInfo auditInfo);
    }
}