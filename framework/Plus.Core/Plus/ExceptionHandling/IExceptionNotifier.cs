using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Plus.ExceptionHandling
{
    public interface IExceptionNotifier
    {
        Task NotifyAsync([NotNull] ExceptionNotificationContext context);
    }
}