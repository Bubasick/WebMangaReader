using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models.DTO;

namespace Business.Abstraction
{
    public interface IPageService
    {
            Task<PageDto> GetById(int id);
            Task<IEnumerable<PageDto>> GetAll();
            Task Add(PageDto pageDto);

    }
}