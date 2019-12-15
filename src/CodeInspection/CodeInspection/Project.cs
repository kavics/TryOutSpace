using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace CodeInspection
{
    [DebuggerDisplay("{Name} ({Issues.Count} issues)")]
    internal class Project
    {
        public string Name { get; }
        public List<Issue> Issues { get; } = new List<Issue>();

        public Project(string name)
        {
            Name = name;
        }

        public static Project Parse(XmlElement element, Context context)
        {
            // <Project Name="SenseNet.BlobStorage">
            //   <Issue ....

            var name = element.Attributes["Name"].Value;
            var project = new Project(name);

            foreach (XmlElement subElement in element.SelectNodes("Issue"))
                project.Issues.Add(Issue.Parse(subElement, project, context));

            return project;
        }
    }
}
