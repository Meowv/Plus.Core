using JetBrains.Annotations;

namespace Plus.Auditing
{
    public interface IAuditingManager
    {
        [CanBeNull]
        IAuditLogScope Current { get; }

        IAuditLogSaveHandle BeginScope();
    }
}