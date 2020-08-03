using Plus.DependencyInjection;
using System.Threading.Tasks;

namespace Plus.ExceptionHandling
{
    [ExposeServices(typeof(IExceptionSubscriber))]
    public abstract class ExceptionSubscriber : IExceptionSubscriber, ITransientDependency
    {
        public abstract Task HandleAsync(ExceptionNotificationContext context);
    }
}