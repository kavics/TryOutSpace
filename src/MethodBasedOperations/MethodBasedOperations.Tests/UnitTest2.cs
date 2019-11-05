using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Candidates()
        {
            var m0 = Class2.AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = Class2.AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = Class2.AddMethod(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = Class2.AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = Class2.GetMethodByRequest("fv1",
                new Dictionary<string, object> { {"a", "asdf" }, {"b", "qwer" }, {"y", 12 }, {"x", 42 } });

            // ASSERT
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(42, context.Parameters["x"]);
        }
    }
}
