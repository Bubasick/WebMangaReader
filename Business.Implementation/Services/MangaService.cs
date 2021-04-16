using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models.DTO;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Implementation.Services
{
    public class MangaService : IMangaService
    {
        private readonly MangaDbContext _dbContext;
        private readonly IMapper _mapper;

        public MangaService( MangaDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<MangaDto> GetById(int id)
        { 
            var result =  await _dbContext.Mangas.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                throw new ArgumentException($"{nameof(id)} is not valid.");
            }
            return _mapper.Map<Manga,MangaDto>(result);
        }
            
        public async Task<IEnumerable<MangaDto>> GetAll()
        {
            var result =  await _dbContext.Mangas.ToListAsync();
            return _mapper.Map<IEnumerable<Manga>,IEnumerable<MangaDto>>(result);
        }

        public async Task Add(MangaDto mangaDto)
        {
            await _dbContext.Mangas.AddAsync(_mapper.Map<MangaDto, Manga>(mangaDto));
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id,MangaDto mangaDto)
        {
            var entityToUpdate = await _dbContext.Mangas.FirstOrDefaultAsync(x => x.Id == id);
            if (entityToUpdate is null)
            {
                throw new ArgumentException($"{nameof(id)} is not valid.");
            }
            _mapper.Map(mangaDto,entityToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(MangaDto mangaDto)
        {
            _dbContext.Mangas.Remove(_mapper.Map<MangaDto, Manga>(mangaDto));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var manga = _dbContext.Mangas.FirstOrDefault(x => x.Id == id);
            _dbContext.Mangas.Remove(manga);
            await _dbContext.SaveChangesAsync();
        }
    }
}