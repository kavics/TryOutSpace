using System.Collections.Generic;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class OperationCallingContext
    {
        public SnAuthorizationEvaluator AuthorizationEvaluator { get; set; } = SnAuthorizationEvaluator.Default;

        public Content Content { get; }
        public OperationInfo Operation { get; }

        public OperationCallingContext(Content content, OperationInfo info)
        {
            Content = content;
            Operation = info;
        }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        internal void SetParameter(string name, object parsed)
        {
            Parameters[name] = parsed;
        }
    }
}

