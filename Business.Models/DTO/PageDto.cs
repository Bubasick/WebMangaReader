using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;

namespace Business.Models.DTO
{
    public class PageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public ChapterDto Chapter { get; set; }    
        public int ChapterId { get; set; }
        public IFormFile Content { get; set; }

        public MyBlobInfo BlobInfo { get; set; }
    }
}