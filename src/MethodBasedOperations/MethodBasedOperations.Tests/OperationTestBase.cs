using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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

        protected Content GetContent(int id = 0, string contentTypeName = null)
        {
            if (id == 0)
                id = 42;
            return new Content(id, new ContentType { Name = contentTypeName ?? "GenericContent" } );
        }

        internal class OperationInspectorSwindler : IDisposable
        {
            private readonly OperationInspector _original;
            public OperationInspectorSwindler(OperationInspector instance)
            {
                _original = OperationInspector.Instance;
                OperationInspector.Instance = instance;
            }

            public void Dispose()
            {
                OperationInspector.Instance = _original;
            }
        }
        internal class AllowEverything : OperationInspector
        {
            private StringBuilder _sb = new StringBuilder();
            public string Log { get { return _sb.ToString(); } }

            public override bool CheckBeforeInvoke(User user, OperationCallingContext context)
            {
                _sb.AppendLine($"CheckBeforeInvoke: {user.Id}, {context.Operation.Method.Name}");
                return true;
            }
            public override bool CheckByPermissions(Content content, User user, string[] permissions)
            {
                _sb.AppendLine($"CheckByPermissions: {content.Id}, {user.Id}, {string.Join(",", permissions)}");
                return true;
            }
            public override bool CheckByRoles(User user, string[] roles)
            {
                _sb.AppendLine($"CheckByRoles: {user.Id}, {string.Join(",", roles)}");
                return true;
            }
        }

    }
}
