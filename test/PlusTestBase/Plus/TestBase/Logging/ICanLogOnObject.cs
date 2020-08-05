using System.Collections.Generic;

namespace Plus.TestBase.Logging
{
    public interface ICanLogOnObject
    {
        List<string> Logs { get; }
    }
}