using System.Collections.Generic;

namespace MethodBasedOperations
{
    public class OperationCallingContext
    {
        public OperationInfo Operation { get; }
        public OperationCallingContext(OperationInfo info)
        {
            Operation = info;
        }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        internal void SetParameter(string name, object parsed)
        {
            Parameters[name] = parsed;
        }
    }
}

