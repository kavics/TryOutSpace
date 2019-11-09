﻿using System.Reflection;
using MethodBasedOperations.Tests.Accessors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class CallingTests : OperationTestBase
    {
        [TestMethod]
        public void Call_RequiredPrimitives()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            var context = OperationCenter.GetMethodByRequest("Op1",
                @"{""a"":""asdf"", ""b"":42, ""c"":true, ""d"":0.12, ""e"":0.13, ""f"":0.14}");

            // ACTION
            var result = OperationCenter.Invoke(new Content(), context);

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
            var context = OperationCenter.GetMethodByRequest("Op2", @"{""dummy"":0}");
            var result = OperationCenter.Invoke(new Content(), context);
            // ASSERT
            var objects = (object[])result;
            Assert.AreEqual(null, objects[0]);
            Assert.AreEqual(0, objects[1]);
            Assert.AreEqual(false, objects[2]);
            Assert.AreEqual(0.0f, objects[3]);
            Assert.AreEqual(0.0m, objects[4]);
            Assert.AreEqual(0.0d, objects[5]);

            // ACTION
            context = OperationCenter.GetMethodByRequest("Op2", @"{""a"":""testvalue""}");
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
            context = OperationCenter.GetMethodByRequest("Op2", @"{""b"":42}");
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
            context = OperationCenter.GetMethodByRequest("Op2", @"{""c"":true}");
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
            context = OperationCenter.GetMethodByRequest("Op2", @"{""d"":12.345}");
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
            context = OperationCenter.GetMethodByRequest("Op2", @"{""e"":12.345}");
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
            context = OperationCenter.GetMethodByRequest("Op2", @"{""f"":12.345}");
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
        public void Call_NullAndDefault()
        {
            Reset();

            AddMethod(typeof(TestOperations).GetMethod("Op1"));
            var context = OperationCenter.GetMethodByRequest("Op1",
                @"{""a"":null, ""b"":null, ""c"":null, ""d"":null, ""e"":null, ""f"":null}");

            // ACTION
            var result = OperationCenter.Invoke(new Content(), context);

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
            var context = OperationCenter.GetMethodByRequest("Op1",
                @"{""a"":undefined, ""b"":undefined, ""c"":undefined, ""d"":undefined, ""e"":undefined, ""f"":undefined}");

            // ACTION
            var result = OperationCenter.Invoke(new Content(), context);

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
