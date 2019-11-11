// ReSharper disable once CheckNamespace
namespace SenseNet.ContentRepository
{
    public class ContentType //UNDONE: MOCK: Delete this class
    {
        public string Name { get; set; }
    }
    public class Content //UNDONE: MOCK: Delete this class
    {
        public ContentType ContentType { get; set; }
    }
}
