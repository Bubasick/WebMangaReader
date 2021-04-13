using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IMangaService
    {
        Task<MangaDto> GetById(int id);
        Task<IEnumerable<MangaDto>> GetAll();
        Task Add(MangaDto mangaDto);
        Task Update(MangaDto mangaDto);
        Task DeleteById(int id);
        Task Delete(MangaDto mangaDto);
    }
}