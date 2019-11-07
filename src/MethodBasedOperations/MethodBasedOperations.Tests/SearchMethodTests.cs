using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Reflection;
using System.Threading;

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
        public void GetMethodByRequest_Bool()
        {
            OperationCenter.Reset();

            var m = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, bool a", null));

            // ACTION-1 strict
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":true}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(true, context.Parameters["a"]);

            // ACTION not strict
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""true""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(true, context.Parameters["a"]);
        }
        [TestMethod]
        public void GetMethodByRequest_Decimal()
        {
            OperationCenter.Reset();

            var m = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, decimal a", null));

            // ACTION-1 strict
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);

            // ACTION not strict, globalized
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789m, context.Parameters["a"]);
        }
        [TestMethod]
        public void GetMethodByRequest_Float()
        {
            OperationCenter.Reset();

            var m = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, float a", null));

            // ACTION-1 strict
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);

            // ACTION not strict, globalized
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789f, context.Parameters["a"]);
        }
        [TestMethod]
        public void GetMethodByRequest_Double()
        {
            OperationCenter.Reset();

            var m = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, double a", null));

            // ACTION-1 strict
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":0.123456789}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);

            // ACTION not strict, localized
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu-hu");
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0,123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);

            // ACTION not strict, globalized
            context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":""0.123456789""}");
            // ASSERT
            Assert.AreEqual(m, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(0.123456789d, context.Parameters["a"]);
        }

        [TestMethod]
        public void GetMethodByRequest_Spaceship()
        {
            OperationCenter.Reset();

            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, Spaceship a", null));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, Elephant a", null));

            // ACTION-1
            var context = OperationCenter.GetMethodByRequest("fv1",
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
        public void GetMethodByRequest_Elephant()
        {
            OperationCenter.Reset();

            var m1 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, Spaceship a", null));
            var m2 = OperationCenter.Discover(new TestMethodInfo("fv1", "Content content, Elephant a", null));

            // ACTION-1
            var context = OperationCenter.GetMethodByRequest("fv1", @"{""a"":{""Snout"":42, ""Height"":44}}");

            // ASSERT
            Assert.AreEqual(m2, context.Operation);
            Assert.AreEqual(1, context.Parameters.Count);
            Assert.AreEqual(typeof(Elephant), context.Parameters["a"].GetType());
            var elephant = (Elephant)context.Parameters["a"];
            Assert.AreEqual(42, elephant.Snout);
            Assert.AreEqual(44, elephant.Height);
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
        public void GetMethodByRequest_UnmatchedOptional()
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
