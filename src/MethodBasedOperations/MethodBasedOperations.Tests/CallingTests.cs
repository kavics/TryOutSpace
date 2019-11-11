using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class CallingTests : OperationTestBase
    {
        internal class AuthorizationEvaluatorSwindler : IDisposable
        {
            private readonly OperationCallingContext _context;
            private readonly SnAuthorizationEvaluator _original;
            public AuthorizationEvaluatorSwindler(OperationCallingContext context, SnAuthorizationEvaluator evaluator)
            {
                _context = context;
                _original = context.AuthorizationEvaluator;
                context.AuthorizationEvaluator = evaluator;
            }

            public void Dispose()
            {
                _context.AuthorizationEvaluator = _original;
            }
        }
        private class AllowEverything : SnAuthorizationEvaluator
        {
            public override bool Evaluate(Content content, User user, OperationCallingContext context)
            {
                return true;
            }
        }

        [TestMethod]
        public void Call_RequiredPrimitives()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            var context = OperationCenter.GetMethodByRequest(GetContent("User"), "Op1",
                @"{""a"":""asdf"", ""b"":42, ""c"":true, ""d"":0.12, ""e"":0.13, ""f"":0.14}");

            // ACTION
            object result;
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);

            // ASSERT
            var objects = (object[])result;
            Assert.AreEqual("asdf", objects[0]);
            Assert.AreEqual(42, objects[1]);
            Assert.AreEqual(true, objects[2]);
            Assert.AreEqual(0.12f, objects[3]);
            Assert.AreEqual(0.13m, objects[4]);
            Assert.AreEqual(0.14d, objects[5]);
        }
        [TestMethod]
        public void Call_OptionalPrimitives()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op2"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""dummy"":0}");
            object result;
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            var objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""a"":""testvalue""}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual("testvalue", objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""b"":42}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(42, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""c"":true}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(true, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""d"":12.345}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(12.345f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""e"":12.345}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(12.345m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""f"":12.345}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(12.345d, objects[5]);
        }

        [TestMethod]
        public void Call_MinimalParameters()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op3"));

            // ACTION
            var context = OperationCenter.GetMethodByRequest(GetContent(), "Op3", @"{""dummy"":1}");
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
            {
                var result = OperationCenter.Invoke(new Content(), context);
                // ASSERT
                Assert.AreEqual("Called", result);
            }

        }

        [TestMethod]
        public void Call_NullAndDefault()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            var context = OperationCenter.GetMethodByRequest(GetContent("User"), "Op1",
                @"{""a"":null, ""b"":null, ""c"":null, ""d"":null, ""e"":null, ""f"":null}");

            // ACTION
            object result;
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);

            // ASSERT
            var objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);
        }
        [TestMethod]
        public void Call_UndefinedAndDefault()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            var context = OperationCenter.GetMethodByRequest(GetContent("User"), "Op1",
                @"{""a"":undefined, ""b"":undefined, ""c"":undefined, ""d"":undefined, ""e"":undefined, ""f"":undefined}");

            // ACTION
            object result;
            using (new AuthorizationEvaluatorSwindler(context, new AllowEverything()))
                result = OperationCenter.Invoke(new Content(), context);

            // ASSERT
            var objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);
        }
    }
}
