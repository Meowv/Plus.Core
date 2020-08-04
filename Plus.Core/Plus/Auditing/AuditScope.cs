using JetBrains.Annotations;

namespace Plus.Auditing
{
    public interface IAuditLogScope
    {
        [NotNull]
        AuditLogInfo Log { get; }
    }
}