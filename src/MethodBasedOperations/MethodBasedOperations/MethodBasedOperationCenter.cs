using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SenseNet.ContentRepository;

namespace MethodBasedOperations
{
    public class MethodBasedOperationInfo
    {
        public MethodBase Method { get; set; }
        public int RequiredParameterCount => ParameterNames.Length - OptionalParameterCount;
        public int OptionalParameterCount { get; set; }
        public string[] ParameterNames { get; set; }
        public Type[] ParameterTypes { get; set; }
    }

    public class MethodBasedOperationCenter
    {
        public static IDictionary<string, MethodBase[]> Discover()
        {
            throw new NotImplementedException();
        }
        public static MethodBasedOperationInfo Discover(MethodBase method)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
                return null;
            if (parameters[0].ParameterType != typeof(Content))
                return null;
            if (parameters[0].IsOptional)
                return null;

            return new MethodBasedOperationInfo
            {
                Method = method,
                ParameterNames = parameters.Select(x => x.Name).ToArray(),
                ParameterTypes = parameters.Select(x => x.ParameterType).ToArray(),
                OptionalParameterCount = parameters.Count(x => x.IsOptional),
            };
        }

        public static string[] GetSignatures(MethodBasedOperationInfo info)
        {
            var baseSignature = $"{info.Method.Name}|{string.Join(",", info.ParameterNames.Take(info.RequiredParameterCount))}";
            return new[] {baseSignature};
        }

        public static IEnumerable<string[]> EnumerateCombinations(string requiredNames, string optionalNames)
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<int[]> EnumerateCombinations(int k)
        {
            for (int n = 1; n <= k; n++)
                foreach (var item in EnumerateCombinations(k, n))
                    yield return item;
        }
        public static IEnumerable<int[]> EnumerateCombinations(int k, int n)
        {
            var digits = new int[n];
            var lastDigit = n - 1;

            for (int i = 1; i < digits.Length; i++)
                digits[i] = digits[i - 1] + 1;

            do
            {
                yield return digits.ToArray();
            } while (Increase(digits, k));
        }

        private static bool Increase(int[] digits, int max)
        {
            var offset = 0;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] < max - offset -1)
                {
                    digits[i]++;
                    for (int j = i + 1; j < digits.Length; j++)
                        digits[j] = digits[j - 1] + 1;
                    return true;
                }
                offset++;
            }
            return false;
        }

        public static int __add(int a, int b)
        {
            return a + b;
        }
    }
}
