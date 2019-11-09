using System;
using System.Collections.Generic;
using System.Text;
using SenseNet.ContentRepository;

namespace MethodBasedOperations.Tests
{
    internal class Elephant
    {
        public int Snout { get; set; }
        public int Height { get; set; }
    }
    internal class Spaceship
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Length { get; set; }
    }

    public class TestOperations
    {
        [ODataFunction]
        [RequiredPermissions("See, Run")]
        [SnAuthorize(roles: "Administrators,Editors")]
        public static object[] Op1(Content content,
            string a, int b, bool c, float d, decimal e, double f)
        {
            return new object[] { a, b, c, d, e, f };
        }
        [ODataFunction]
        [RequiredPermissions("P1, P2")]
        public static object[] Op2(Content content,
            string a = null, int b = 0, bool c = false, float d = 0f, decimal e = 0m, double f = 0d)
        {
            return new object[] { a, b, c, d, e, f };
        }
    }
}
