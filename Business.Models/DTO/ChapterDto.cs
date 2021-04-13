using System.Collections.Generic;

namespace Business.Models.DTO
{
    public class ChapterDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MangaDto Manga { get; set; }
        public int MangaId { get; set; }
        public ICollection<PageDto> Pages { get; set; }
    }
}