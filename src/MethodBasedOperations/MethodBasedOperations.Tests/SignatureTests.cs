using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class SignatureTests
    {
        [TestMethod]
        public void MBO_GetInfo_0Prm()
        {
            var method = new TestMethodInfo("fv1", null, null);

            var info = OperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm_Invalid()
        {
            var method = new TestMethodInfo("fv1", "string a", null);

            var info = OperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm_Optional()
        {
            var method = new TestMethodInfo("fv1", null, "Content content");

            var info = OperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm()
        {
            var method = new TestMethodInfo("fv1", "Content content", null);

            var info = OperationCenter.Discover(method);

            Assert.AreEqual(0, info.RequiredParameterNames.Length);
            Assert.AreEqual(0, info.RequiredParameterTypes.Length);
            Assert.AreEqual(0, info.OptionalParameterNames.Length);
            Assert.AreEqual(0, info.OptionalParameterTypes.Length);
        }
        [TestMethod]
        public void MBO_GetInfo_5Prm2()
        {
            var method = new TestMethodInfo("fv1", "Content content, string a, int b", "string c, DateTime d");

            // ACTION
            var info = OperationCenter.Discover(method);

            // ASSERT
            Assert.AreEqual(method, info.Method);
            Assert.AreEqual(2, info.OptionalParameterNames.Length);
            Assert.AreEqual(
                "a,b",
                string.Join(",", info.RequiredParameterNames));
            Assert.AreEqual(
                "String,Int32",
                string.Join(",", info.RequiredParameterTypes.Select(t => t.Name).ToArray()));
            Assert.AreEqual(
                "c,d",
                string.Join(",", info.OptionalParameterNames));
            Assert.AreEqual(
                "String,DateTime",
                string.Join(",", info.OptionalParameterTypes.Select(t => t.Name).ToArray()));
        }

    }
}
