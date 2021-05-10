using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Business.Models.DTO
{
    public class MangaDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime FinishDate { get; set; }
        public ICollection<ChapterDto> Chapters { get; set; }
        public IFormFile ChaptersArchive { get; set; }
    }
}