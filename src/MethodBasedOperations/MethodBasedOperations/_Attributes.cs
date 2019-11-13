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
        public string Policy { get; set; }
        public string Role { get; set; }

        public SnAuthorizeAttribute() { }
        public SnAuthorizeAttribute(string policy)
        {
            this.Policy = policy;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RequiredPermissionAttribute : Attribute
    {
        public string Permission { get; set; }

        public RequiredPermissionAttribute() { }
        public RequiredPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScenarioAttribute : Attribute
    {
        public string Scenario { get; set; }

        public ScenarioAttribute() { }
        public ScenarioAttribute(string scenario)
        {
            Scenario = scenario;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ContentTypeAttribute : Attribute
    {
        public string ContentTypeName { get; set; }

        public ContentTypeAttribute() { }
        public ContentTypeAttribute(string contentType)
        {
            ContentTypeName = contentType;
        }
    }
}
