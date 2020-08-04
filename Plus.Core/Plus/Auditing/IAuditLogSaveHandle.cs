using System;
using System.Threading.Tasks;

namespace Plus.Auditing
{
    public interface IAuditLogSaveHandle : IDisposable
    {
        Task SaveAsync();
    }
}