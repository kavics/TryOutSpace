using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class CombinationTests
    {
        [TestMethod]
        public void MBO_Combinations_Names_1_0()
        {
            Assert.AreEqual("p,q|a,p,q|b,p,q|a,b,p,q", GetCombinations("fv1", "p,q", "a,b"));
        }

        [TestMethod]
        public void MBO_Combinations_1()
        {
            Assert.AreEqual("0", GetCombinations(1, 1));
        }
        [TestMethod]
        public void MBO_Combinations_2()
        {
            Assert.AreEqual("0|1|0,1", GetCombinations(2));
        }
        [TestMethod]
        public void MBO_Combinations_3()
        {
            Assert.AreEqual("0|1|2|0,1|0,2|1,2|0,1,2", GetCombinations(3));
        }
        [TestMethod]
        public void MBO_Combinations_4()
        {
            Assert.AreEqual("0|1|2|3|0,1|0,2|0,3|1,2|1,3|2,3|0,1,2|0,1,3|0,2,3|1,2,3|0,1,2,3", GetCombinations(4));
        }
        [TestMethod]
        public void MBO_Combinations_5()
        {
            Assert.AreEqual("0|1|2|3|4|0,1|0,2|0,3|0,4|1,2|1,3|1,4|2,3|2,4|3,4|0,1,2|0,1,3|" +
                            "0,1,4|0,2,3|0,2,4|0,3,4|1,2,3|1,2,4|1,3,4|2,3,4|0,1,2,3|0,1,2,4|0,1,3,4|" +
                            "0,2,3,4|1,2,3,4|0,1,2,3,4", GetCombinations(5));
        }

        [TestMethod]
        public void MBO_Combinations_1_1()
        {
            Assert.AreEqual("0", GetCombinations(1, 1));
        }
        [TestMethod]
        public void MBO_Combinations_2_1()
        {
            Assert.AreEqual("0|1", GetCombinations(2, 1));
        }
        [TestMethod]
        public void MBO_Combinations_2_2()
        {
            Assert.AreEqual("0,1", GetCombinations(2, 2));
        }
        [TestMethod]
        public void MBO_Combinations_3_1()
        {
            Assert.AreEqual("0|1|2", GetCombinations(3, 1));
        }
        [TestMethod]
        public void MBO_Combinations_3_2()
        {
            Assert.AreEqual("0,1|0,2|1,2", GetCombinations(3, 2));
        }
        [TestMethod]
        public void MBO_Combinations_3_3()
        {
            Assert.AreEqual("0,1,2", GetCombinations(3, 3));
        }
        [TestMethod]
        public void MBO_Combinations_4_1()
        {
            Assert.AreEqual("0|1|2|3", GetCombinations(4, 1));
        }
        [TestMethod]
        public void MBO_Combinations_4_2()
        {
            Assert.AreEqual("0,1|0,2|0,3|1,2|1,3|2,3", GetCombinations(4, 2));
        }
        [TestMethod]
        public void MBO_Combinations_4_3()
        {
            Assert.AreEqual("0,1,2|0,1,3|0,2,3|1,2,3", GetCombinations(4, 3));
        }
        [TestMethod]
        public void MBO_Combinations_4_4()
        {
            Assert.AreEqual("0,1,2,3", GetCombinations(4, 4));
        }
        [TestMethod]
        public void MBO_Combinations_5_1()
        {
            Assert.AreEqual("0|1|2|3|4", GetCombinations(5, 1));
        }
        [TestMethod]
        public void MBO_Combinations_5_2()
        {
            Assert.AreEqual("0,1|0,2|0,3|0,4|1,2|1,3|1,4|2,3|2,4|3,4", GetCombinations(5, 2));
        }
        [TestMethod]
        public void MBO_Combinations_5_3()
        {
            Assert.AreEqual("0,1,2|0,1,3|0,1,4|0,2,3|0,2,4|0,3,4|1,2,3|1,2,4|1,3,4|2,3,4", GetCombinations(5, 3));
        }
        [TestMethod]
        public void MBO_Combinations_5_4()
        {
            Assert.AreEqual("0,1,2,3|0,1,2,4|0,1,3,4|0,2,3,4|1,2,3,4", GetCombinations(5, 4));
        }
        [TestMethod]
        public void MBO_Combinations_5_5()
        {
            Assert.AreEqual("0,1,2,3,4", GetCombinations(5, 5));
        }

        /* =============================================== */

        public string GetCombinations(int k)
        {
            var indexes = MethodBasedOperationCenter.EnumerateCombinations(k).ToArray();
            return string.Join("|",
                indexes.Select(y => string.Join(",",
                    y.Select(x => x.ToString()).ToArray())));
        }

        public string GetCombinations(int k, int n)
        {
            var indexes = MethodBasedOperationCenter.EnumerateCombinations(k, n).ToArray();
            return string.Join("|",
                indexes.Select(y => string.Join(",",
                    y.Select(x => x.ToString()).ToArray())));
        }

        private string GetCombinations(string methodName, string reqNames, string optNames)
        {
            var requiredNames = string.IsNullOrEmpty(reqNames)
                ? new string[0]
                : reqNames.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            var optionalNames = string.IsNullOrEmpty(optNames)
                ? new string[0]
                : optNames.Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

            var keys = MethodBasedOperationCenter.EnumerateCombinations(reqNames, optNames);

            return string.Join("|",
                keys.Select(y => string.Join(",",
                    y.Select(x => x.ToString()).ToArray())));
        }

    }
}
