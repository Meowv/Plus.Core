using System;
using System.Runtime.Serialization;

namespace Plus
{
    public class PlusInitializationException : PlusException
    {
        public PlusInitializationException()
        {

        }

        public PlusInitializationException(string message)
            : base(message)
        {

        }

        public PlusInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public PlusInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}