using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class SnAuthorizationEvaluator
    {
        public static readonly SnAuthorizationEvaluator Default = new SnAuthorizationEvaluator();

        public virtual bool Evaluate(Content content, User user, OperationCallingContext context)
        {
            return false;
        }
    }
}
