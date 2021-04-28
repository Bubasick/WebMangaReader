using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Data.Entities
{
    public class Chapter : BaseEntity
    {
        
        public string Name { get; set; }
        public Manga Manga { get; set; }
        public int MangaId { get; set; }
        public ICollection<Page> Pages { get; set; }
    }
}