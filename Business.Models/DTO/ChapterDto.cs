using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Business.Models.DTO
{
    public class ChapterDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MangaDto Manga { get; set; }
        public int MangaId { get; set; }
        public ICollection<PageDto> Pages { get; set; }
        public IFormFile PageArchive{ get; set; }
    }
}