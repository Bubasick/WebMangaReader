namespace Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Chapter Chapter { get; set; }
        public int ChapterId { get; set; }
    }
}