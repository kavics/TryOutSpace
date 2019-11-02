using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var method = new TestMethodInfo("fv1", "Content content, string a, int b", "string c, DateTime d");

            // ACTION
            var info = Class1.Discover(method);

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
    }
}
