using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MethodBasedOperations
{
    [Serializable]
    public class SnNotSupportedException : Exception //UNDONE: Delete this class
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
