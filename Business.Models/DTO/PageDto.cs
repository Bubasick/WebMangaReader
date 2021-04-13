using System;

namespace Business.Models.DTO
{
    public class PageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public ChapterDto Chapter { get; set; }    
        public int ChapterId { get; set; }
    }
}