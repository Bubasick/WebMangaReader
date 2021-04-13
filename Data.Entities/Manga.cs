using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Manga : BaseEntity
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public DateTime FinishDate { get; set; }
        public ICollection<Chapter> Chapters { get; set; }  
    }
}