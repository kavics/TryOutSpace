using System.Collections.Generic;
using System.Linq;

namespace CodeInspection
{
    internal class Context
    {
        public List<string> IssueCategories { get; } = new List<string>();
        public List<string> Severities { get; } = new List<string>();
        public List<IssueType> IssueTypes { get; } = new List<IssueType>();
        public List<Project> Projects { get; } = new List<Project>();

        public int GetIssueCount()
        {
            var count = Projects.SelectMany(p => p.Issues).Count();
            return count;
        }
    }
}
