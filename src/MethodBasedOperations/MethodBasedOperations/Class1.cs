using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MethodBasedOperations
{
    public class MethodBasedOperationInfo
    {
        public MethodBase Method { get; set; }
        public int OptionalParameterCount { get; set; }
        public string[] ParameterNames { get; set; }
        public Type[] ParameterTypes { get; set; }
    }

    public class Class1
    {
        public static IDictionary<string, MethodBase[]> Discover()
        {
            throw new NotImplementedException();
        }
        public static MethodBasedOperationInfo Discover(MethodBase method)
        {
            var prms = method.GetParameters();
            return new MethodBasedOperationInfo
            {
                Method = method,
                ParameterNames = prms.Select(x => x.Name).ToArray(),
                ParameterTypes = prms.Select(x => x.ParameterType).ToArray(),
                OptionalParameterCount = prms.Count(x => x.IsOptional)
            };
        }
    }
}
