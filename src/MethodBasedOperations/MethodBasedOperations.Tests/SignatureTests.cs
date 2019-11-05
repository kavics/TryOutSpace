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

            var info = MethodBasedOperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm_Invalid()
        {
            var method = new TestMethodInfo("fv1", "string a", null);

            var info = MethodBasedOperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm_Optional()
        {
            var method = new TestMethodInfo("fv1", null, "Content content");

            var info = MethodBasedOperationCenter.Discover(method);

            Assert.IsNull(info);
        }
        [TestMethod]
        public void MBO_GetInfo_1Prm()
        {
            var method = new TestMethodInfo("fv1", "Content content", null);

            var info = MethodBasedOperationCenter.Discover(method);

            Assert.AreEqual(1, info.ParameterNames.Length);
            Assert.AreEqual(1, info.ParameterTypes.Length);
            Assert.AreEqual(1, info.RequiredParameterCount);
            Assert.AreEqual(0, info.OptionalParameterCount);
        }
        [TestMethod]
        public void MBO_GetInfo_5Prm2()
        {
            var method = new TestMethodInfo("fv1", "Content content, string a, int b", "string c, DateTime d");

            // ACTION
            var info = MethodBasedOperationCenter.Discover(method);

            // ASSERT
            Assert.AreEqual(method, info.Method);
            Assert.AreEqual(2, info.OptionalParameterCount);
            Assert.AreEqual(
                "content,a,b,c,d",
                string.Join(",", info.ParameterNames));
            Assert.AreEqual(
                "Content,String,Int32,String,DateTime",
                string.Join(",", info.ParameterTypes.Select(t => t.Name).ToArray()));
        }

        [TestMethod]
        public void MBO_GetSignature_1Prm()
        {
            var method = new TestMethodInfo("fv1", "Content content", null);
            var info = MethodBasedOperationCenter.Discover(method);

            // ACTION
            var signatures = MethodBasedOperationCenter.GetSignatures(info);

            // ASSERT
            Assert.AreEqual(1, signatures.Length);
            Assert.AreEqual("fv1|content", signatures[0]);
        }
        [TestMethod]
        public void MBO_GetSignature_3Prm()
        {
            var method = new TestMethodInfo("fv1", "Content content, string a, int b", null);
            var info = MethodBasedOperationCenter.Discover(method);

            // ACTION
            var signatures = MethodBasedOperationCenter.GetSignatures(info);

            // ASSERT
            Assert.AreEqual(1, signatures.Length);
            Assert.AreEqual("fv1|content,a,b", signatures[0]);
        }
        [TestMethod]
        public void MBO_GetSignature_3Prm1()
        {
            var method = new TestMethodInfo("fv1", "Content content, string a, int b", "DateTime c");
            var info = MethodBasedOperationCenter.Discover(method);

            // ACTION
            var signatures = MethodBasedOperationCenter.GetSignatures(info);

            // ASSERT
            Assert.AreEqual(2, signatures.Length);
            Assert.AreEqual("fv1|content,a,b", signatures[0]);
            Assert.AreEqual("fv1|content,a,b,c", signatures[0]);
        }
    }
}
