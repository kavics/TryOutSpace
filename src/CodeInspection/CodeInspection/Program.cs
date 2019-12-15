using System;
using System.Diagnostics;
using System.Xml;

namespace CodeInspection
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new[] { @"D:\Code Issues in 'SenseNet'.xml" };
            //args = new[] { @"D:\_code-issues\sensenet.xml" };
            args = new[] { @"D:\_code-issues\sn-security.xml" };
            //args = new[] { @"D:\_code-issues\sn-tools.xml" };

            var context = new Context();
            Run(args[0], context);
            var count = context.GetIssueCount();

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void Run(string codeIssuesPath, Context context)
        {
            var xml = new XmlDocument();
            xml.Load(codeIssuesPath);

            ParseIssueTypes(xml, context);
            ParseProjects(xml, context);
        }

        private static void ParseIssueTypes(XmlDocument xml, Context context)
        {
            foreach (XmlElement element in xml.SelectNodes("/Report/IssueTypes/IssueType"))
                context.IssueTypes.Add(IssueType.Parse(element, context));
        }

        private static void ParseProjects(XmlDocument xml, Context context)
        {
            foreach (XmlElement element in xml.SelectNodes("/Report/Issues/Project"))
                context.Projects.Add(Project.Parse(element, context));
        }
    }
}
