// ReSharper disable once CheckNamespace

using MethodBasedOperations;

namespace SenseNet.ContentRepository
{
    public class ContentType //UNDONE: MOCK: Delete this class
    {
        public string Name { get; set; }
    }
    public class Content //UNDONE: MOCK: Delete this class
    {
        public ContentType ContentType { get; }
        public int Id { get; }

        public Content(int id, ContentType contentType)
        {
            Id = id;
            ContentType = contentType;
        }
    }
}
