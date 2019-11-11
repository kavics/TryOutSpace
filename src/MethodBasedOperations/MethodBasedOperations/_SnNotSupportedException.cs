using System;
using System.Runtime.Serialization;

namespace MethodBasedOperations
{
    [Serializable]
    public class SnNotSupportedException : Exception //UNDONE: MOCK: Delete this class
    {
        public SnNotSupportedException()
        {
        }

        public SnNotSupportedException(string message) : base(message)
        {
        }

        public SnNotSupportedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SnNotSupportedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
