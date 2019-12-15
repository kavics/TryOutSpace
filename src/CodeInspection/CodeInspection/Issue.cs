using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace CodeInspection
{
    [DebuggerDisplay("{Type.Id} {Message}")]
    internal class Issue
    {
        public IssueType Type { get; }
        public Project Project { get; }
        public string File { get; }
        public string Offset { get; }
        public string Line { get; }
        public string Message { get; }

        public Issue(IssueType type, Project project, string file, string offset, string line, string message)
        {
            Type = type;
            Project = project;
            Message = message;
            File = file;
            Offset = offset;
            Line = line;
        }

        public static Issue Parse(XmlElement element, Project project, Context context)
        {
            // <Project Name="SenseNet.BlobStorage">
            //   <Issue
            //       TypeId="CommentTypo"
            //       File="Tests\SnAdminRuntime.Tests\Implementations\TestDisk.cs"
            //       Offset="2739-2748"
            //       Line="68"
            //       Message="Typo in comment: &quot;webfolder&quot;" />

            var typeId = element.Attributes["TypeId"].Value;
            var type = context.IssueTypes.First(x => x.Id == typeId);
            var message = element.Attributes["Message"].Value;
            var file = element.Attributes["File"].Value;
            var offset = element.Attributes["Offset"].Value;
            var line = element.Attributes["Line"]?.Value;

            return new Issue(type, project, file, offset, line, message);
        }
    }
}
