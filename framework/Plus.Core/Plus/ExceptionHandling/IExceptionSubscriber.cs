using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.ExceptionHandling
{
    public interface IExceptionSubscriber
    {
        Task HandleAsync([NotNull] ExceptionNotificationContext context);
    }
}
