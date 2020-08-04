using Microsoft.Extensions.Logging;
using Plus.Logging;
using System;
using System.Runtime.Serialization;

namespace Plus.Authorization
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// </summary>
    [Serializable]
    public class PlusAuthorizationException : PlusException, IHasLogLevel
    {
        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Creates a new <see cref="PlusAuthorizationException"/> object.
        /// </summary>
        public PlusAuthorizationException()
        {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// Creates a new <see cref="PlusAuthorizationException"/> object.
        /// </summary>
        public PlusAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="PlusAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public PlusAuthorizationException(string message)
            : base(message)
        {
            LogLevel = LogLevel.Warning;
        }

        /// <summary>
        /// Creates a new <see cref="PlusAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public PlusAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
            LogLevel = LogLevel.Warning;
        }
    }
}