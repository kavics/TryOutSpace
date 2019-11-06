using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MethodBasedOperations
{
    public class OperationCallingContext
    {
        public MethodBasedOperationInfo2 Operation { get; }
        public OperationCallingContext(MethodBasedOperationInfo2 info)
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

