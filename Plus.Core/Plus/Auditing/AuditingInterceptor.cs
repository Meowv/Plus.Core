using Plus.Aspects;
using Plus.DependencyInjection;
using Plus.DynamicProxy;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Plus.Auditing
{
    public class AuditingInterceptor : PlusInterceptor, ITransientDependency
    {
        private readonly IAuditingHelper _auditingHelper;
        private readonly IAuditingManager _auditingManager;

        public AuditingInterceptor(IAuditingHelper auditingHelper, IAuditingManager auditingManager)
        {
            _auditingHelper = auditingHelper;
            _auditingManager = auditingManager;
        }

        public override async Task InterceptAsync(IPlusMethodInvocation invocation)
        {
            if (!ShouldIntercept(invocation, out var auditLog, out var auditLogAction))
            {
                await invocation.ProceedAsync();
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                await invocation.ProceedAsync();
            }
            catch (Exception ex)
            {
                auditLog.Exceptions.Add(ex);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditLogAction.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                auditLog.Actions.Add(auditLogAction);
            }
        }

        protected virtual bool ShouldIntercept(
            IPlusMethodInvocation invocation,
            out AuditLogInfo auditLog,
            out AuditLogActionInfo auditLogAction)
        {
            auditLog = null;
            auditLogAction = null;

            if (PlusCrossCuttingConcerns.IsApplied(invocation.TargetObject, PlusCrossCuttingConcerns.Auditing))
            {
                return false;
            }

            var auditLogScope = _auditingManager.Current;
            if (auditLogScope == null)
            {
                return false;
            }

            if (!_auditingHelper.ShouldSaveAudit(invocation.Method))
            {
                return false;
            }

            auditLog = auditLogScope.Log;
            auditLogAction = _auditingHelper.CreateAuditLogAction(
                auditLog,
                invocation.TargetObject.GetType(),
                invocation.Method,
                invocation.Arguments
            );

            return true;
        }
    }
}