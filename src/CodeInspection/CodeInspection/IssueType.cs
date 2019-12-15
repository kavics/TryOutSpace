using System.Diagnostics;
using System.Xml;

namespace CodeInspection
{
    [DebuggerDisplay("{Id} {CategoryId} {Severity}")]
    internal class IssueType
    {
        public string Id { get; }
        public string CategoryId { get; }
        public string Category { get; }
        public string SubCategory { get; }
        public string Severity { get; }

        public IssueType(string id, string categoryId, string severity)
        {
            Id = id;
            CategoryId = categoryId;
            Severity = severity;
        }

        public static IssueType Parse(XmlElement element, Context context)
        {
            // <IssueType
            //     Id="MemberCanBeMadeStatic.Local"
            //     Category="Common Practices and Code Improvements"
            //     CategoryId="BestPractice"
            //     SubCategory="Member can be made static(shared)"
            //     Description="Member can be made static(shared): Private accessibility"
            //     Severity="HINT"
            //     WikiUrl="https://www.jetbrains.com/resharperplatform/help?Keyword=MemberCanBeMadeStatic.Local" />


            var id = element.Attributes["Id"].Value;
            var categoryId = element.Attributes["CategoryId"].Value;
            //var category = element.Attributes["Category"].Value;
            //var subCategory = element.Attributes["SubCategory"]?.Value;
            var severity = element.Attributes["Severity"].Value;

            int categoryIdIndex;
            if ((categoryIdIndex = context.IssueCategories.IndexOf(categoryId)) < 0)
            {
                categoryIdIndex = context.IssueCategories.Count;
                context.IssueCategories.Add(categoryId);
            }

            int severityIndex;
            if ((severityIndex = context.Severities.IndexOf(severity)) < 0)
            {
                severityIndex = context.Severities.Count;
                context.Severities.Add(severity);
            }

            return new IssueType(
                id,
                context.IssueCategories[categoryIdIndex],
                context.Severities[severityIndex]
            );
        }
    }
}
