using System;

namespace Data.Entities
{
    public class Page : BaseEntity
    {

        public string Name { get; set; }
        public Guid Guid { get; set; }
        public Chapter Chapter { get; set; }
        public int ChapterId { get; set; }
        public string ContentType { get; set; }
    }

}