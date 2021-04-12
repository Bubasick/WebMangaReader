using System.Collections.Generic;

namespace Data.Entities
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}