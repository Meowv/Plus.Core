using NUglify;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Plus.Minify.NUglify
{
    public class NUglifyException : PlusException
    {
        public List<UglifyError> Errors { get; set; }

        public NUglifyException(string message, List<UglifyError> errors)
            : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public NUglifyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}