using Plus.ExceptionHandling;
using System;
using System.Runtime.Serialization;

namespace Plus.Http.Client
{
    [Serializable]
    public class PlusRemoteCallException : PlusException, IHasErrorCode, IHasErrorDetails
    {
        public string Code => Error?.Code;

        public string Details => Error?.Details;

        public RemoteServiceErrorInfo Error { get; set; }

        public PlusRemoteCallException()
        {

        }

        public PlusRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public PlusRemoteCallException(RemoteServiceErrorInfo error)
            : base(error.Message)
        {
            Error = error;
        }
    }
}