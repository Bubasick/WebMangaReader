using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IChapterService
    {
        Task<ChapterDto> GetById(int id);   
        Task<IEnumerable<ChapterDto>> GetAll();
        Task<IEnumerable<string>> GetAllPagesLinksById(int id);
        Task Add(ChapterDto chapterDto);
        Task Update(int id, ChapterDto chapterDto);
        Task DeleteById(int id);
        Task Delete(ChapterDto chapterDto);
    }
}