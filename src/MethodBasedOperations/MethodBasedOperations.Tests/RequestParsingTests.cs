using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MethodBasedOperations.Tests
{
    [TestClass]
    public class RequestParsingTests
    {
        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void Request_InvalidJson()
        {
            var request = OperationCenter.Read("asdf");
        }
        [TestMethod]
        public void Request_Invalid()
        {
            var request = OperationCenter.Read("");
            Assert.IsNull(request);

            request = OperationCenter.Read("['asdf']");
            Assert.IsNull(request);
        }
        [TestMethod]
        public void Request_Object()
        {
            var request = OperationCenter.Read("{'a':'asdf'}");
            Assert.AreEqual(JTokenType.String, request["a"].Type);
            Assert.AreEqual("{\"a\":\"asdf\"}", request.ToString()
                .Replace("\r", "").Replace("\n", "").Replace(" ", ""));
        }
        [TestMethod]
        public void Request_Models()
        {
            var request = OperationCenter.Read("models=[{'a':'asdf'}]");
            Assert.AreEqual(JTokenType.String, request["a"].Type);
            Assert.AreEqual("{\"a\":\"asdf\"}", request.ToString()
                .Replace("\r", "").Replace("\n", "").Replace(" ", ""));
        }
        [TestMethod]
        public void Request_Properties()
        {
            var request = OperationCenter.Read("models=[{'a':42, 'b':false, 'c':'asdf', 'd':[], 'e':{a:12}}]");
            Assert.AreEqual(JTokenType.Integer, request["a"].Type);
            Assert.AreEqual(JTokenType.Boolean, request["b"].Type);
            Assert.AreEqual(JTokenType.String, request["c"].Type);
            Assert.AreEqual(JTokenType.Array, request["d"].Type);
            Assert.AreEqual(JTokenType.Object, request["e"].Type);
        }
        [TestMethod]
        public void Request_Float()
        {
            var request = OperationCenter.Read("models=[{'a':4.2}]");
            Assert.AreEqual(JTokenType.Float, request["a"].Type);
            Assert.AreEqual(4.2f, request["a"].Value<float>());
            Assert.AreEqual(4.2d, request["a"].Value<double>());
            Assert.AreEqual(4.2m, request["a"].Value<decimal>());
        }
    }
}
