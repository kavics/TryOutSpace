﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;
// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedVariable

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class SearchMethodTests
    {
        [TestMethod]
        public void GetMethodByRequest_Strict_1()
        {
            OperationCenter.Reset();

            var m0 = OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = OperationCenter.Discover(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv1",
                @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":42}");

            // ASSERT
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(42, context.Parameters["x"]);
        }
        [TestMethod]
        public void GetMethodByRequest_Strict_2()
        {
            OperationCenter.Reset();

            var m0 = OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = OperationCenter.Discover(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv1",
                @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""42"" }");

            // ASSERT
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual("42", context.Parameters["x"]);
        }
        [TestMethod]
        public void GetMethodByRequest_NotStrict_1()
        {
            OperationCenter.Reset();

            var m0 = OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "bool x"));
            var m3 = OperationCenter.Discover(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION-1
            var context = OperationCenter.GetMethodByRequest("fv1",
                @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""42""}");

            // ASSERT-1
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(42, context.Parameters["x"]);

            // ACTION-2
            context = OperationCenter.GetMethodByRequest("fv1",
                @"{ ""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""true""}");

            // ASSERT-2
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(true, context.Parameters["x"]);
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void GetMethodByRequest_NotFound_ByName()
        {
            OperationCenter.Reset();

            OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void GetMethodByRequest_NotFound_ByRequiredParamName()
        {
            OperationCenter.Reset();

            OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a, string b", null));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv0", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void GetMethodByRequest_NotFound_ByRequiredParamType()
        {
            OperationCenter.Reset();

            OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a, string b", null));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv0", @"{""a"":""asdf"",""b"":42}");
        }
        [TestMethod]
        [ExpectedException(typeof(AmbiguousMatchException))]
        public void GetMethodByRequest_AmbiguousMatch()
        {
            OperationCenter.Reset();

            var m0 = OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = OperationCenter.Discover(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void GetMethodByRequest_sss()
        {
            OperationCenter.Reset();

            var m0 = OperationCenter.Discover(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, string a", "bool x"));
            var m3 = OperationCenter.Discover(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest("fv1", @"{ ""a"":""asdf"",""x"":""asdf""}");
        }
    }
}
