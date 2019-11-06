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

