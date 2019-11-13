using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SenseNet.ContentRepository;

namespace MethodBasedOperations.Tests
{
    public class TestMethodInfo : MethodBase
    {
        private ParameterInfo[] _parameters;
        public TestMethodInfo(string name, string requiredParameters, string optionalParameters)
        {
            Name = name;
            _parameters = ParseParameters(requiredParameters, optionalParameters);
        }
        private ParameterInfo[] ParseParameters(string requiredParameters, string optionalParameters)
        {
            var p = 0;
            var parameters =
                requiredParameters?.Split(',').Select(x => ParseParameter(p++, x, true)).ToArray() ?? new ParameterInfo[0];
            if(optionalParameters != null)
            {
                var optionals = optionalParameters.Split(',').Select(x => ParseParameter(p++, x, false)).ToArray();
                parameters = parameters.Union(optionals).ToArray();
            }
            return parameters;
        }
        private ParameterInfo ParseParameter(int position, string src, bool required)
        {
            var terms = src.Trim().Split(' ');
            var type = ParseType(terms[0].Trim());
            return new TestParameterInfo(position, type, terms[1].Trim(), !required);
        }
        private Type ParseType(string src)
        {
            switch (src)
            {
                case "Content": return typeof(Content);
                case "string": return typeof(string);
                case "int": return typeof(int);
                case "DateTime": return typeof(DateTime);
                case "double": return typeof(double);
                case "decimal": return typeof(decimal);
                case "float": return typeof(float);
                case "bool": return typeof(bool);

                case "Elephant": return typeof(Elephant);
                case "Spaceship": return typeof(Spaceship);

                default:
                    throw new ApplicationException("Unknown type: " + src);
            }
        }

        /* ======================================================================================= */

        public override ParameterInfo[] GetParameters() => _parameters;


        public override MethodAttributes Attributes => throw new NotImplementedException();

        public override RuntimeMethodHandle MethodHandle => throw new NotImplementedException();

        public override Type DeclaringType => throw new NotImplementedException();

        public override MemberTypes MemberType => throw new NotImplementedException();

        public override string Name { get; }

        public override Type ReflectedType => throw new NotImplementedException();

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override MethodImplAttributes GetMethodImplementationFlags()
        {
            throw new NotImplementedException();
        }

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}
