using System;
using System.Runtime.Serialization;

namespace Plus
{
    public class PlusShutdownException : PlusException
    {
        public PlusShutdownException()
        {

        }

        public PlusShutdownException(string message)
            : base(message)
        {

        }

        public PlusShutdownException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public PlusShutdownException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}