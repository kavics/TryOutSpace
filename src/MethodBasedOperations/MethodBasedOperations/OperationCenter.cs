﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class OperationCenter
    {
        private static readonly OperationInfo[] EmptyMethods = new OperationInfo[0];
        private static readonly Dictionary<string, OperationInfo[]> Methods =
            new Dictionary<string, OperationInfo[]>();

        public static void Reset() //UNDONE: Delete this method
        {
            Methods.Clear();
        }

        public static IDictionary<string, OperationInfo[]> Discover()
        {
            throw new NotImplementedException();
        }
        public static OperationInfo Discover(MethodBase method)
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
            var info = new OperationInfo
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
        private static void AddMethod(OperationInfo info)
        {
            // This is a custom dynamic array implementation. 
            // Reason: The single / overloaded method rate probably very high (a lot of single vs a few overloads).
            // Therefore the usual List<T> approach is ineffective because the most List<T> item will contain
            // many unnecessary empty pointers.
            if (!Methods.TryGetValue(info.Method.Name, out var methods))
            {
                methods = new[] {info};
                Methods.Add(info.Method.Name, methods);
            }
            else
            {
                var copy = new OperationInfo[methods.Length + 1];
                methods.CopyTo(copy, 0);
                copy[copy.Length - 1] = info;
                Methods[info.Method.Name] = copy;
            }
        }

        public static OperationCallingContext GetMethodByRequest(string methodName, string requestBody)
        {
            return GetMethodByRequest(methodName, Read(requestBody));
        }
        public static OperationCallingContext GetMethodByRequest(string methodName, JObject requestParameters)
        {
            var requestParameterNames = requestParameters.Properties().Select(p => p.Name).ToArray();

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

        private static OperationInfo[] GetCandidatesByName(string methodName)
        {
            if (Methods.TryGetValue(methodName, out var methods))
                return methods;
            return EmptyMethods;
        }
        private static bool AllRequiredParametersExist(OperationInfo info, string[] requestParameterNames)
        {
            foreach (var requiredParameterName in info.RequiredParameterNames)
                if (!requestParameterNames.Contains(requiredParameterName))
                    return false;
            return true;
        }
        private static bool TryParseParameters(OperationInfo candidate, JObject requestParameters, bool strict, out OperationCallingContext context)
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
        private static bool TryParseParameter(string name, Type type, JToken token, bool strict, out object parsed)
        {
            if (type == GetTypeAndValue(type, token, out parsed))
                return true;

            if (!strict)
            {
                if (token.Type == JTokenType.String)
                {
                    var stringValue = token.Value<string>();
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
                    if (type == typeof(decimal))
                    {
                        if (decimal.TryParse(stringValue, out var v))
                        {
                            parsed = v;
                            return true;
                        }
                        if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                        {
                            parsed = v;
                            return true;
                        }
                    }
                    if (type == typeof(float))
                    {
                        if (float.TryParse(stringValue, out var v))
                        {
                            parsed = v;
                            return true;
                        }
                        if (float.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                        {
                            parsed = v;
                            return true;
                        }
                    }
                    if (type == typeof(double))
                    {
                        if (double.TryParse(stringValue, out var v))
                        {
                            parsed = v;
                            return true;
                        }
                        if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                        {
                            parsed = v;
                            return true;
                        }
                    }
                    //TODO: try parse further opportunities from string to "type"
                }
            }

            parsed = null;
            return false;
        }
        private static readonly JsonSerializer ValueDeserializer = JsonSerializer.Create(
            new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
        private static Type GetTypeAndValue(Type expectedType, JToken token, out object value)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    value = token.Value<string>();
                    return typeof(string);
                case JTokenType.Integer:
                    value = token.Value<int>();
                    return typeof(int);
                case JTokenType.Boolean:
                    value = token.Value<bool>();
                    return typeof(bool);
                case JTokenType.Float:
                    if (expectedType == typeof(float))
                    {
                        value = token.Value<float>();
                        return typeof(float);
                    }
                    if (expectedType == typeof(decimal))
                    {
                        value = token.Value<decimal>();
                        return typeof(decimal);
                    }
                    value = token.Value<double>();
                    return typeof(double);

                case JTokenType.Object:
                    try
                    {
                        value = token.ToObject(expectedType, ValueDeserializer);
                        return expectedType;
                    }
                    catch (JsonSerializationException)
                    {
                        value = null;
                        return typeof(object);
                    }

                //UNDONE: handle array
                //case JTokenType.Array: break;

                case JTokenType.None:
                case JTokenType.Null:
                case JTokenType.Undefined:
                    value = expectedType.IsValueType ? Activator.CreateInstance(expectedType) : null;
                    return expectedType;

                case JTokenType.Date:
                case JTokenType.Guid:
                case JTokenType.TimeSpan:
                case JTokenType.Uri:
                    value = token.Value<string>();
                    return typeof(string);

                //case JTokenType.Constructor:
                //case JTokenType.Property:
                //case JTokenType.Comment:
                //case JTokenType.Raw:
                //case JTokenType.Bytes:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private static string GetRequestSignature(string methodName, IEnumerable<string> parameterNames)
        {
            return $"{methodName}({string.Join(",", parameterNames)})";
        }
        private static string GetMethodSignatures(List<OperationCallingContext> contexts)
        {
            return string.Join(", ", contexts.Select(c => c.Operation.ToString()));
        }

        public static object Invoke(Content content, OperationCallingContext context)
        {
            var method = context.Operation.Method;
            var methodParams = method.GetParameters();
            var paramValues = new object[methodParams.Length];
            paramValues[0] = content;
            for (int i = 1; i < methodParams.Length; i++)
                if (!context.Parameters.TryGetValue(methodParams[i].Name, out paramValues[i]))
                    paramValues[i] = methodParams[i].DefaultValue;

            return method.Invoke(null, paramValues);
        }

        /* ====================================================================== */

        /// <summary>
        /// Helper method for deserializing the given string representation.
        /// </summary>
        /// <param name="models">JSON object that will be deserialized.</param>
        /// <returns>Deserialized JObject instance.</returns>
        public static JObject Read(string models)
        {
            if (string.IsNullOrEmpty(models))
                return null;

            var firstChar = models.Last() == ']' ? '[' : '{';
            var p = models.IndexOf(firstChar);
            if (p > 0)
                models = models.Substring(p);

            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var serializer = JsonSerializer.Create(settings);
            var jReader = new JsonTextReader(new StringReader(models));
            var deserialized = serializer.Deserialize(jReader);

            if (deserialized is JObject jObject)
                return jObject;
            if (deserialized is JArray jArray)
                return jArray[0] as JObject;

            throw new SnNotSupportedException();
        }
    }
}
