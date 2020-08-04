using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.Auditing
{
    [Dependency(TryRegister = true)]
    public class SimpleLogAuditingStore : IAuditingStore, ISingletonDependency
    {
        public ILogger<SimpleLogAuditingStore> Logger { get; set; }

        public SimpleLogAuditingStore()
        {
            Logger = NullLogger<SimpleLogAuditingStore>.Instance;
        }

        public Task SaveAsync(AuditLogInfo auditInfo)
        {
            Logger.LogInformation(auditInfo.ToString());
            return Task.FromResult(0);
        }
    }
}