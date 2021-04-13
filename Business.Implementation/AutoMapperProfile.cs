using AutoMapper;
using Business.Models.DTO;
using Data.Entities;
using Manga = Data.Entities.Manga;

namespace Business.Implementation
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Manga, MangaDto>().ReverseMap();
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<Chapter, ChapterDto>().ReverseMap();
        }
        
    }
}