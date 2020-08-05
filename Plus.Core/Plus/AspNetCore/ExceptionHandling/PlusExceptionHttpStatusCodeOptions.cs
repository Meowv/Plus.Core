using System.Collections.Generic;
using System.Net;

namespace Plus.AspNetCore.ExceptionHandling
{
    public class PlusExceptionHttpStatusCodeOptions
    {
        public IDictionary<string, HttpStatusCode> ErrorCodeToHttpStatusCodeMappings { get; }

        public PlusExceptionHttpStatusCodeOptions()
        {
            ErrorCodeToHttpStatusCodeMappings = new Dictionary<string, HttpStatusCode>();
        }

        public void Map(string errorCode, HttpStatusCode httpStatusCode)
        {
            ErrorCodeToHttpStatusCodeMappings[errorCode] = httpStatusCode;
        }
    }
}