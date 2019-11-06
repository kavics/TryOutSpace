﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class OperationCenter
    {
        private static readonly MethodBasedOperationInfo2[] EmptyMethods = new MethodBasedOperationInfo2[0];
        private static readonly Dictionary<string, MethodBasedOperationInfo2[]> Methods =
            new Dictionary<string, MethodBasedOperationInfo2[]>();

        public static IDictionary<string, MethodBasedOperationInfo2[]> Discover()
        {
            throw new NotImplementedException();
        }
        public static MethodBasedOperationInfo2 Discover(MethodBase method)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
                return null;
            if (parameters[0].ParameterType != typeof(Content))
                return null;
            if (parameters[0].IsOptional)
                return null;

            parameters = parameters.Skip(1).ToArray();
            var req = parameters.Where(x => !x.IsOptional).ToArray();
            var opt = parameters.Where(x => x.IsOptional).ToArray();
            var info = new MethodBasedOperationInfo2
            {
                Method = method,
                RequiredParameterNames = req.Select(x => x.Name).ToArray(),
                RequiredParameterTypes = req.Select(x => x.ParameterType).ToArray(),
                OptionalParameterNames = opt.Select(x => x.Name).ToArray(),
                OptionalParameterTypes = opt.Select(x => x.ParameterType).ToArray(),
            };
            AddMethod(info);
            return info;
        }
        private static void AddMethod(MethodBasedOperationInfo2 info)
        {
            // This is a custom dynamic array implementation. 
            // Reason: The single / overloaded method rate probably very high (a lot of single vs a few overloads).
            // Therefore the usual List<T> approach is ineffecive because the most List<T> item will contain
            // many unnecessary empty pointers.
            if (!Methods.TryGetValue(info.Method.Name, out var methods))
            {
                methods = new[] {info};
                Methods.Add(info.Method.Name, methods);
            }
            else
            {
                var copy = new MethodBasedOperationInfo2[methods.Length + 1];
                methods.CopyTo(copy, 0);
                copy[copy.Length - 1] = info;
                Methods[info.Method.Name] = copy;
            }
        }

        public static OperationCallingContext GetMethodByRequest(string methodName, Dictionary<string, object> requestParameters)
        {
            var requestParameterNames = requestParameters.Keys;

            var candidates = GetCandidatesByName(methodName);
            candidates = candidates.Where(x => AllRequiredParametersExist(x, requestParameterNames)).ToArray();

            // If there is no any candidates, throw: Operation not found ERROR
            if (candidates.Length == 0)
                throw new OperationNotFoundException("Operation not found: " + GetRequestSignature(methodName, requestParameterNames));

            // Search candidates by parameter types
            // Phase-1: search complete type match (strict)
            var contexts = new List<OperationCallingContext>();
            foreach(var candidate in candidates)
                if (TryParseParameters(candidate, requestParameters, true, out var context))
                    contexts.Add(context);

            if (contexts.Count == 0)
            {
                // Phase-2: search convertible type match
                foreach (var candidate in candidates)
                    if (TryParseParameters(candidate, requestParameters, false, out var context))
                        contexts.Add(context);
            }

            if (contexts.Count == 0)
                throw new OperationNotFoundException("Operation not found: " + GetRequestSignature(methodName, requestParameterNames));

            if (contexts.Count > 1)
                throw new AmbiguousMatchException($"Ambiguous call: {GetRequestSignature(methodName, requestParameterNames)} --> {GetMethodSignatures(contexts)}");

            return contexts[0];
        }

        private static MethodBasedOperationInfo2[] GetCandidatesByName(string methodName)
        {
            if (Methods.TryGetValue(methodName, out var methods))
                return methods;
            return EmptyMethods;
        }
        private static bool AllRequiredParametersExist(MethodBasedOperationInfo2 info, IEnumerable<string> requestParameterNames)
        {
            foreach (var requiredParameterName in info.RequiredParameterNames)
                if (!requestParameterNames.Contains(requiredParameterName))
                    return false;
            return true;
        }
        private static bool TryParseParameters(MethodBasedOperationInfo2 candidate, Dictionary<string, object> requestParameters, bool strict, out OperationCallingContext context)
        {
            context = new OperationCallingContext(candidate);

            // Foreach all optional parameters of the method
            for (int i = 0; i < candidate.OptionalParameterNames.Length; i++)
            {
                var name = candidate.OptionalParameterNames[i];

                // If does not exist in the request: continue (move the next parameter)
                if (!requestParameters.TryGetValue(name, out var value))
                    continue;

                // If parse request by parameter"s type is not successful: return false
                var type = candidate.OptionalParameterTypes[i];
                if (!TryParseParameter(name, type, value, strict, out var parsed))
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

                // If parse request by parameter"s type is not successful: return false
                if (!TryParseParameter(name, type, value, strict, out var parsed))
                    return false;

                // Add parameter name/value to the calling context
                context.SetParameter(name, parsed);
            }
            return true;
        }
        private static bool TryParseParameter(string name, Type type, object value, bool strict, out object parsed)
        {
            if (value.GetType() == type)
            {
                parsed = value;
                return true;
            }

            if (!strict)
            {
                if (value is string stringValue)
                {
                    if (type == typeof(int))
                    {
                        if (int.TryParse(stringValue, out var v))
                        {
                            parsed = v;
                            return true;
                        }
                    }
                    if (type == typeof(bool))
                    {
                        if (bool.TryParse(stringValue, out var v))
                        {
                            parsed = v;
                            return true;
                        }
                    }
                    //UNDONE: try parse further opportunities from string to "type"
                }
            }

            parsed = null;
            return false;
        }
        private static string GetRequestSignature(string methodName, IEnumerable<string> parameterNames)
        {
            return $"{methodName}({string.Join(",", parameterNames)})";
        }
        private static string GetMethodSignatures(List<OperationCallingContext> contexts)
        {
            return string.Join(", ", contexts.Select(c => c.Operation.ToString()));
        }

        public static void Reset() //UNDONE: Delete this method
        {
            Methods.Clear();
        }
    }
}