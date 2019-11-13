using System;
using System.Reflection;

namespace MethodBasedOperations.Tests
{
    internal class TestParameterInfo : ParameterInfo
    {
        public TestParameterInfo(int position, Type parameterType, string name, bool isOptional)
        {
            PositionImpl = position;
            NameImpl = name;
            ClassImpl = parameterType;
            if (isOptional)
                AttrsImpl |= ParameterAttributes.Optional;
        }
    }
}
