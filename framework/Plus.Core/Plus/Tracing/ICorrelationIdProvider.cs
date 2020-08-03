using JetBrains.Annotations;

namespace Plus.Tracing
{
    public interface ICorrelationIdProvider
    {
        [NotNull]
        string Get();
    }
}
