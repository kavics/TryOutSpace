using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MethodBasedOperations
{
    public class MethodBasedOperationInfo2
    {
        public MethodBase Method { get; set; }
        public int OptionalParameterCount { get; set; }
        public string[] ParameterNames { get; set; }
        public Type[] ParameterTypes { get; set; }
    }

    public class MethodBasedOperationCallingContext
    {
        MethodBasedOperationInfo2 _info;
        public MethodBasedOperationCallingContext(MethodBasedOperationInfo2 info)
        {
            _info = info;
        }
    }

    public class Class2
    {
        private static List<MethodBasedOperationInfo2> _methods = new List<MethodBasedOperationInfo2>();

        public static MethodBasedOperationInfo2 AddMethod(MethodBase method)
        {
            var prms = method.GetParameters();
            var info = new MethodBasedOperationInfo2
            {
                Method = method,
                ParameterNames = prms.Select(x => x.Name).ToArray(),
                ParameterTypes = prms.Select(x => x.ParameterType).ToArray(),
                OptionalParameterCount = prms.Count(x => x.IsOptional)
            };
            _methods.Add(info);
            return info;
        }

        public static object GetMethodByRequest(string methodName, Dictionary<string, object> requestParameters)
        {
            var requestParameterNames = requestParameters.Keys;

            //    Get operation info candidates by member name -> Candidates
            var candidates = GetCandidatesByName(methodName);

            //    Foreach candidates
            //      If any required parameter name is missing
            //	    Remove from candidates
            candidates = candidates.Where(x => AllRequiredParametersExist(x, requestParameterNames)).ToArray();

            //    If there is no any candidates
            //      Return: Method not found ERROR
            if (candidates.Length == 0)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));

            //    Foreach candidates
            for (int i = candidates.Length - 1; i >= 0; i--)
            {
                //      Create a calling context: method, parameter dictionary: name/value
                var candidate = candidates[i];

                //      Parse all (required/optional) parameters by method's types and memorize the values.
                //        If there is any parser ERROR
                //	      Remove from candidates
                MethodBasedOperationCallingContext lastContext;
                candidates = candidates.Where(x => TryParseParameters(x, requestParameters, out lastContext)).ToArray();
            }

            //    If there is no any candidates
            //      Return: Method not found ERROR
            if (candidates.Length == 0)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));

            //    If there are more than one candidates
            //      Return: Ambiguous call ERROR
            if (candidates.Length > 1)
                throw new ApplicationException("Method not found: " + GetSignature(methodName, requestParameterNames));


            throw new NotImplementedException();
        }

        private static bool TryParseParameters(MethodBasedOperationInfo2 candidate, Dictionary<string, object> requestParameters, out MethodBasedOperationCallingContext context)
        {
            //      Foreach all (required/optional) parameters of the method
            //        If parameter is optional and does not exist in the request
            //	      continue (move the next parameter)
            //	    If parse request by parameter"s type is successful
            //	      Add parameter name/value to the calling context
            //	    else
            //	      Remove from candidates
            //	      break (move the next candidate)

            throw new NotImplementedException();
        }

        private static bool AllRequiredParametersExist(MethodBasedOperationInfo2 arg, IEnumerable<string> requestParameterNames)
        {
            throw new NotImplementedException();
        }

        private static MethodBasedOperationInfo2[] GetCandidatesByName(string methodName)
        {
            return _methods.Where(m => m.Method.Name == methodName).ToArray();
        }

        private static string GetSignature(string methodName, IEnumerable<string> parameterNames)
        {
            return $"{methodName}({string.Join(",", parameterNames)})";
        }
    }
}

