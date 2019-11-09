using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MethodBasedOperations
{
    public class ODataActionAttribute : Attribute
    {
    }
    public class ODataFunctionAttribute : Attribute
    {
    }
    public class SnAuthorizeAttribute : Attribute
    {
        public string Role { get; set; }
        public SnAuthorizeAttribute(string roles)
        {
            this.Role = roles;
        }
    }
    public class RequiredPermissionsAttribute : Attribute
    {
        public string Permissions { get; }
        public RequiredPermissionsAttribute(string permissions)
        {
            Permissions = permissions;
        }
    }
}
