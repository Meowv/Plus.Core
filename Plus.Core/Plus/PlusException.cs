using System;
using System.Runtime.Serialization;

namespace Plus
{
    /// <summary>
    /// Base exception type for those are thrown by Plus system for Plus specific exceptions.
    /// </summary>
    public class PlusException : Exception
    {
        public PlusException()
        {

        }

        public PlusException(string message)
            : base(message)
        {

        }

        public PlusException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public PlusException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}