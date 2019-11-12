using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;
// ReSharper disable StringLiteralTypo

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class CallingTests : OperationTestBase
    {
        [TestMethod]
        public void OD_MBO_Call_RequiredPrimitives()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            OperationCallingContext context;
            using(new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(111,"User"), "Op1",
                    @"{""a"":""asdf"", ""b"":42, ""c"":true, ""d"":0.12, ""e"":0.13, ""f"":0.14}");

            // ACTION
            object result;
            using (new OperationInspectorSwindler(new AllowEverything()))
                result = OperationCenter.Invoke(context);

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
        public void OD_MBO_Call_OptionalPrimitives()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op2"));

            // ACTION
            OperationCallingContext context;
            object result;
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""dummy"":0}");
                result = OperationCenter.Invoke(context);
            }
            // ASSERT
            var objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""a"":""testvalue""}");
                result = OperationCenter.Invoke(context);
            }
            // ASSERT
            objects = (object[]) result;
            Assert.AreEqual("testvalue", objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""b"":42}");
                result = OperationCenter.Invoke(context);
            }
            // ASSERT
            objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(42, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""c"":true}");
                result = OperationCenter.Invoke(context);
            }
            // ASSERT
            objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(true, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""d"":12.345}");
                result = OperationCenter.Invoke(context);
            }
            // ASSERT
            objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(12.345f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""e"":12.345}");
                result = OperationCenter.Invoke(context);
            }

            // ASSERT
            objects = (object[]) result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(12.345m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                context = OperationCenter.GetMethodByRequest(GetContent(), "Op2", @"{""f"":12.345}");
                result = OperationCenter.Invoke(context);
            }

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
        public void OD_MBO_Call_MinimalParameters()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op3"));

            // ACTION
            using (new OperationInspectorSwindler(new AllowEverything()))
            {
                var context = OperationCenter.GetMethodByRequest(GetContent(), "Op3", @"{""dummy"":1}");
                var result = OperationCenter.Invoke(context);
                // ASSERT
                Assert.AreEqual("Called", result);
            }

        }

        [TestMethod]
        public void OD_MBO_Call_NullAndDefault()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(111, "User"), "Op1",
                @"{""a"":null, ""b"":null, ""c"":null, ""d"":null, ""e"":null, ""f"":null}");

            // ACTION
            object result;
            using (new OperationInspectorSwindler(new AllowEverything()))
                result = OperationCenter.Invoke(context);

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
        public void OD_MBO_Call_UndefinedAndDefault()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            OperationCallingContext context;
            using (new OperationInspectorSwindler(new AllowEverything()))
                context = OperationCenter.GetMethodByRequest(GetContent(111, "User"), "Op1",
                @"{""a"":undefined, ""b"":undefined, ""c"":undefined, ""d"":undefined, ""e"":undefined, ""f"":undefined}");

            // ACTION
            object result;
            using (new OperationInspectorSwindler(new AllowEverything()))
                result = OperationCenter.Invoke(context);

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
        public void OD_MBO_Call_Inspection()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));

            var inspector = new AllowEverything();
            var content = GetContent(111, "User");

            // ACTION
            using (new OperationInspectorSwindler(inspector))
            {
                var context = OperationCenter.GetMethodByRequest(content, "Op1",
                    @"{""a"":""asdf"", ""b"":42, ""c"":true, ""d"":0.12, ""e"":0.13, ""f"":0.14}");
                var result = OperationCenter.Invoke(context);
            }

            // ASSERT
            var lines = inspector.Log.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual("CheckByRoles: 1, Administrators,Editors", lines[0]);
            Assert.AreEqual("CheckByPermissions: 111, 1, See,Run", lines[1]);
            Assert.AreEqual("CheckBeforeInvoke: 1, Op1", lines[2]);
        }
    }
}
