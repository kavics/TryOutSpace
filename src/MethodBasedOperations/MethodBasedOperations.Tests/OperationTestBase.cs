using System;
using System.Collections.Generic;
using System.Reflection;
using MethodBasedOperations.Tests.Accessors;
using SenseNet.ContentRepository;

namespace MethodBasedOperations.Tests
{
    public abstract class OperationTestBase
    {
        protected static TypeAccessor OperationCenterAccessor = new TypeAccessor(typeof(OperationCenter));
        private readonly Attribute[] _defaultAttributes = new Attribute[] {new ODataFunctionAttribute()};

        protected void Reset()
        {
            var cache = (Dictionary<string, OperationInfo[]>)OperationCenterAccessor.GetStaticField("Operations");
            cache.Clear();
        }
        protected OperationInfo AddMethod(MethodInfo method)
        {
            return (OperationInfo)OperationCenterAccessor.InvokeStatic("AddMethod", method);
        }
        protected OperationInfo AddMethod(TestMethodInfo method)
        {
            return (OperationInfo)OperationCenterAccessor.InvokeStatic("AddMethod", method, _defaultAttributes);
        }

        protected Content GetContent(string contentTypeName = null)
        {
            return new Content { ContentType = new ContentType { Name = contentTypeName ?? "GenericContent" } };
        }

    }
}
