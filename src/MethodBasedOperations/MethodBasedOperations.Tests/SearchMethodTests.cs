using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Reflection;
using System.Threading;
using MethodBasedOperations.Tests.Accessors;

// ReSharper disable StringLiteralTypo
// ReSharper disable UnusedVariable

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class SearchMethodTests : OperationTestBase
    {
        [TestMethod]
        public void OD_MBO_Discover()
        {
            Reset();

            OperationCenter.Discover();

            var discovered = (Dictionary<string, OperationInfo[]>)new TypeAccessor(typeof(OperationCenter)).GetStaticField("Operations");
            //UNDONE: Not finished test.
        }

        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Strict_1()
        {
            Reset();

            var m0 = AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1",
                @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":42}");

            // ASSERT
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(42, context.Parameters["x"]);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Strict_2()
        {
            Reset();

            var m0 = AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1",
                    @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""42"" }");

            // ASSERT
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual("42", context.Parameters["x"]);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Bool()
        {
            Reset();

            var m = AddMethod(new TestMethodInfo("fv1", "Content content, bool a", null));

            // ACTION-1 strict
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":true}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(true, context.Parameters["a"]);

            // ACTION not strict
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""true""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(true, context.Parameters["a"]);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Decimal()
        {
            Reset();

            var m = AddMethod(new TestMethodInfo("fv1", "Content content, decimal a", null));

            // ACTION-1 strict
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);

            // ACTION not strict, globalized
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Float()
        {
            Reset();

            var m = AddMethod(new TestMethodInfo("fv1", "Content content, float a", null));

            // ACTION-1 strict
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);

            // ACTION not strict, globalized
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Double()
        {
            Reset();

            var m = AddMethod(new TestMethodInfo("fv1", "Content content, double a", null));

            // ACTION-1 strict
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);

            // ACTION not strict, globalized
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);
        }

        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Spaceship()
        {
            Reset();

            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, Spaceship a", null));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, Elephant a", null));

            // ACTION-1
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1",
                    @"{""a"":{""Name"":""Space Bender 8"", ""Class"":""Big F Vehicle"", ""Length"":444}}");

            // ASSERT
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(typeof(Spaceship), context.Parameters["a"].GetType());
            var spaceship = (Spaceship)context.Parameters["a"];
            Assert.AreEqual("Space Bender 8", spaceship.Name);
            Assert.AreEqual("Big F Vehicle", spaceship.Class);
            Assert.AreEqual(444, spaceship.Length);
        }
        [TestMethod]
        public void OD_MBO_GetMethodByRequest_Elephant()
        {
            Reset();

            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, Spaceship a", null));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, Elephant a", null));

            // ACTION-1
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":{""Snout"":42, ""Height"":44}}");

            // ASSERT
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(typeof(Elephant), context.Parameters["a"].GetType());
            var elephant = (Elephant)context.Parameters["a"];
            Assert.AreEqual(42, elephant.Snout);
            Assert.AreEqual(44, elephant.Height);
        }


        [TestMethod]
        public void OD_MBO_GetMethodByRequest_NotStrict_1()
        {
            Reset();

            var m0 = AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "bool x"));
            var m3 = AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION-1
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1",
                    @"{""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""42""}");

            // ASSERT-1
            Assert.AreEqual(m1, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(42, context.Parameters["x"]);

            // ACTION-2
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1",
                    @"{ ""a"":""asdf"",""b"":""qwer"",""y"":12,""x"":""true""}");

            // ASSERT-2
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(2, context.Parameters.Count);
            Assert.AreEqual("asdf", context.Parameters["a"]);
            Assert.AreEqual(true, context.Parameters["x"]);
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void OD_MBO_GetMethodByRequest_NotFound_ByName()
        {
            Reset();

            AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void OD_MBO_GetMethodByRequest_NotFound_ByRequiredParamName()
        {
            Reset();

            AddMethod(new TestMethodInfo("fv0", "Content content, string a, string b", null));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "fv0", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void OD_MBO_GetMethodByRequest_NotFound_ByRequiredParamType()
        {
            Reset();

            AddMethod(new TestMethodInfo("fv0", "Content content, string a, string b", null));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "fv0", @"{""a"":""asdf"",""b"":42}");
        }
        [TestMethod]
        [ExpectedException(typeof(AmbiguousMatchException))]
        public void OD_MBO_GetMethodByRequest_AmbiguousMatch()
        {
            Reset();

            var m0 = AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "string x"));
            var m3 = AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{""a"":""asdf""}");
        }
        [TestMethod]
        [ExpectedException(typeof(OperationNotFoundException))]
        public void OD_MBO_GetMethodByRequest_UnmatchedOptional()
        {
            Reset();

            var m0 = AddMethod(new TestMethodInfo("fv0", "Content content, string a", "int x"));
            var m1 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "int x"));
            var m2 = AddMethod(new TestMethodInfo("fv1", "Content content, string a", "bool x"));
            var m3 = AddMethod(new TestMethodInfo("fv2", "Content content, string a", "int x"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "fv1", @"{ ""a"":""asdf"",""x"":""asdf""}");
        }
    }
}
