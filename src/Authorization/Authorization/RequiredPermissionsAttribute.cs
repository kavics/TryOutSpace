using System;

namespace Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiredPermissionsAttribute : Attribute
    {
        public string Role { get; }
        public string Permissions { get; }
        public RequiredPermissionsAttribute(string role, string permissions)
        {
            Role = role;
            Permissions = permissions;
        }
    }
}
