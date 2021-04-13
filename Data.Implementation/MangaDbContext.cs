using Microsoft.EntityFrameworkCore;
using System;
using Data.Entities;

namespace Data.Implementation
{
    public class MangaDbContext  : DbContext
    {
        public MangaDbContext(DbContextOptions<MangaDbContext> options) : base(options)
        {
        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Page> Pages { get; set; }

    }
}
