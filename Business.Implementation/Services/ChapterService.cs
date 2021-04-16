using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models.DTO;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation.Services
{
    public class ChapterService : IChapterService
    {
        private readonly MangaDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChapterService(MangaDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<ChapterDto> GetById(int id)
        {
            var result = await _dbContext.Chapters.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                throw new ArgumentException($"{nameof(id)} is not valid.");
            }
            return _mapper.Map<Chapter, ChapterDto>(result);
        }

        public async Task<IEnumerable<ChapterDto>> GetAll()
        {
            var result = await _dbContext.Chapters.ToListAsync();
            return _mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterDto>>(result);
        }

        public async Task Add(ChapterDto chapterDto)
        {
            await _dbContext.Chapters.AddAsync(_mapper.Map<ChapterDto, Chapter>(chapterDto));
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, ChapterDto chapterDto)
        {
            var entityToUpdate = await _dbContext.Chapters.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToUpdate is null)
            {
                throw new ArgumentException($"{nameof(id)} is not valid.");
            }
            _mapper.Map(chapterDto, entityToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ChapterDto chapterDto)
        {
            _dbContext.Chapters.Remove(_mapper.Map<ChapterDto, Chapter>(chapterDto));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var manga = _dbContext.Chapters.FirstOrDefault(x => x.Id == id);
            _dbContext.Chapters.Remove(manga);
            await _dbContext.SaveChangesAsync();
        }
    }
}
