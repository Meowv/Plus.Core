using System;

namespace Plus.Data
{
    public class PlusDbConcurrencyException : PlusException
    {
        /// <summary>
        /// Creates a new <see cref="PlusDbConcurrencyException"/> object.
        /// </summary>
        public PlusDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PlusDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public PlusDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="PlusDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public PlusDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}