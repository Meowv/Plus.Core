using JetBrains.Annotations;

namespace Plus
{
    public interface IOnApplicationShutdown
    {
        void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);
    }
}