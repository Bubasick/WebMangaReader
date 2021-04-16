using System.IO;

namespace Business.Models
{
    public class MyBlobInfo
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }

        public MyBlobInfo(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }
    }
}