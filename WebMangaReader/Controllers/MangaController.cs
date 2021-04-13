using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Models.DTO;

namespace WebMangaReader.Controllers
{
    [Route("{mangas}")]
    public class MangaController : Controller
    {
        private readonly IMangaService _service;

        public MangaController(IMangaService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddManga([FromBody] MangaDto manga)
        { 
            await _service.Add(manga);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManga([FromBody] MangaDto manga)
        {
            await _service.Update(manga);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromBody] MangaDto manga)
        {
            await _service.Delete(manga);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _service.DeleteById(id);
            return Ok();
        }
    }
}
