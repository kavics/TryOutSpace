using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MethodBasedOperations
{
    [DebuggerDisplay("{ToString()}")]
    public class MethodBasedOperationInfo2
    {
        public MethodBase Method { get; set; }
        public string[] RequiredParameterNames { get; set; }
        public Type[] RequiredParameterTypes { get; set; }
        public string[] OptionalParameterNames { get; set; }
        public Type[] OptionalParameterTypes { get; set; }

        private string[] _empty = new string[0];
        public override string ToString()
        {
            var allNames = (RequiredParameterNames ?? _empty).Union((OptionalParameterNames ?? _empty).Select(x => x + "?")).ToArray();
            var parameters = string.Join(",", allNames);
            return $"{Method.Name}({parameters})";
        }
    }

    public class MethodBasedOperationCallingContext
    {
        public MethodBasedOperationInfo2 Operation { get; }
        public MethodBasedOperationCallingContext(MethodBasedOperationInfo2 info)
        {
            Operation = info;
        }

        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        internal void SetParameter(string name, object parsed)
        {
            Parameters[name] = parsed;
        }
    }

    public class Class2
    {
        private static List<MethodBasedOperationInfo2> _methods = new List<MethodBasedOperationInfo2>();

        public static MethodBasedOperationInfo2 AddMethod(MethodBase method)
        {
            var prms = method.GetParameters().Skip(1); //UNDONE: parameters need to be checked: Required Content is the first.
            var req = prms.Where(x => !x.IsOptional);
            var opt = prms.Where(x => x.IsOptional);
            var info = new MethodBasedOperationInfo2
            {
                Method = method,
                RequiredParameterNames = req.Select(x => x.Name).ToArray(),
                RequiredParameterTypes = req.Select(x => x.ParameterType).ToArray(),
                OptionalParameterNames = opt.Select(x => x.Name).ToArray(),
                OptionalParameterTypes = opt.Select(x => x.ParameterType).ToArray(),
            };
            _methods.Add(info);
            return info;
        }

        public static MethodBasedOperationCallingContext GetMethodByRequest(string methodName, Dictionary<string, object> requestParameters)
        {
            var requestParameterNames = requestParameters.Keys;

            // Get operation info candidates by member name -> Candidates
            var candidates = GetCandidatesByName(methodName);

            // Foreach candidates
            //   If any required parameter name is missing
            //   Remove from candidates
            candidates = candidates.Where(x => AllRequiredParametersExist(x, requestParameterNames)).ToArray();

            //    If there is no any candidates
            //      Return: Method not found ERROR
            if (candidates.Length == 0)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));

            // Foreach candidates
            MethodBasedOperationCallingContext lastContext = null;
            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                // Create a calling context: method, parameter dictionary: name/value
                var candidate = candidates[i];

                // Parse all (required/optional) parameters by method's types and memorize the values.
                //   If there is any parser ERROR
                //   Remove from candidates
                candidates = candidates.Where(x => TryParseParameters(x, requestParameters, out lastContext)).ToArray();
            }

            //    If there is no any candidates
            //      Return: Method not found ERROR
            if (candidates.Length == 0)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));

            // If there are more than one candidates
            //   Return: Ambiguous call ERROR
            if (candidates.Length > 1)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));

            return lastContext;
        }

        private static MethodBasedOperationInfo2[] GetCandidatesByName(string methodName)
        {
            return _methods.Where(m => m.Method.Name == methodName).ToArray();
        }
        private static bool AllRequiredParametersExist(MethodBasedOperationInfo2 info, IEnumerable<string> requestParameterNames)
        {
            foreach (var requiredParameterName in info.RequiredParameterNames)
                if (!requestParameterNames.Contains(requiredParameterName))
                    return false;
            return true;
        }
        private static bool TryParseParameters(MethodBasedOperationInfo2 candidate, Dictionary<string, object> requestParameters, out MethodBasedOperationCallingContext context)
        {
            context = new MethodBasedOperationCallingContext(candidate);

            // Foreach all optional parameters of the method
            for (int i = 0; i < candidate.OptionalParameterNames.Length; i++)
            {
                var name = candidate.OptionalParameterNames[i];

                // If does not exist in the request
                //   continue (move the next parameter)
                if (!requestParameters.TryGetValue(name, out var value))
                    continue;

                // If parse request by parameter"s type is not successful
                //   return false
                var type = candidate.OptionalParameterTypes[i];
                if (!TryParseParameter(name, type, value, out var parsed))
                    return false;

                // Add parameter name/value to the calling context
                context.SetParameter(name, parsed);
            }
            // Foreach all required parameters of the method
            for (int i = 0; i < candidate.RequiredParameterNames.Length; i++)
            {
                var name = candidate.RequiredParameterNames[i];
                var value = requestParameters[name];
                var type = candidate.RequiredParameterTypes[i];

                // If parse request by parameter"s type is not successful
                //    return false
                if (!TryParseParameter(name, type, value, out var parsed))
                    return false;

                // Add parameter name/value to the calling context
                context.SetParameter(name, parsed);
            }
            return true;
        }
        private static bool TryParseParameter(string name, Type type, object value, out object parsed)
        {
            if (value.GetType() == type)
            {
                parsed = value;
                return true;
            }
            if (value is string stringValue)
            {
                if(type == typeof(int))
                {
                    if(int.TryParse(stringValue, out var intValue))
                    {
                        parsed = intValue;
                        return true;
                    }
                }
                //UNDONE: try parse further opportunities from string to "type"
            }
            parsed = null;
            return false;
        }

        private static string GetSignature(string methodName, IEnumerable<string> parameterNames)
        {
            return $"{methodName}({string.Join(",", parameterNames)})";
        }
    }
}

