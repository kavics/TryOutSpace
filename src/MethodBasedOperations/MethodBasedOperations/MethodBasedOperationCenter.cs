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

        //public static IEnumerable<string[]> EnumerateCombinations(string requiredNames, string optionalNames)
        //{

        //}
        //public static IEnumerable<int[]> EnumerateCombinations(int k)
        //{

        //}
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

    }
}
/*
    |  required  | optional  |
    content, x, y, a, b, c, d
    -------------------------
0   content, x, y
1   content, x, y, a
2   content, x, y, b
2   content, x, y, c
2   content, x, y, d
3   content, x, y, a, b
4   content, x, y, a, c
5   content, x, y, a, d
6   content, x, y, b, c
7   content, x, y, b, d
8   content, x, y, c, d
9   content, x, y, a, b, c
10  content, x, y, a, b, d
11  content, x, y, a, c, d
12  content, x, y, b, c, d
13  content, x, y, a, b, c, d
===============================

*/
