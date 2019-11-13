using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace MethodBasedOperations
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class OperationNotFoundException : Exception
    {
        public OperationNotFoundException()
        {
        }

        public OperationNotFoundException(string message) : base(message)
        {
        }

        public OperationNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OperationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
