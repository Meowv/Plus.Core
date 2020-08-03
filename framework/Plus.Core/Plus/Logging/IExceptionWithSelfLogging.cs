using Microsoft.Extensions.Logging;

namespace Plus.Logging
{
    public interface IExceptionWithSelfLogging
    {
        void Log(ILogger logger);
    }
}