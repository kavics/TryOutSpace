using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class OperationInspector
    {
        public static OperationInspector Instance { get; set; } = new OperationInspector();

        public virtual bool CheckBeforeInvoke(User user, OperationCallingContext context)
        {
            return false;
        }

        public virtual bool CheckByRoles(User user, string[] roles)
        {
            //UNDONE: call appropriate method of sensenet.
            return false;
        }

        public virtual bool CheckByPermissions(Content content, User user, string[] permissions)
        {
            //UNDONE: call appropriate method of sensenet.
            return false;
        }
    }
}
