using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Models.DTO;

namespace WebMangaReader.Controllers
{
    [Route("api/[controller]")]
    public class ChapterController : Controller
    {
        private readonly IChapterService _service;

        public ChapterController(IChapterService service)
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
        public async Task<IActionResult> AddChapter([FromBody] ChapterDto chapter)
        {
            await _service.Add(chapter);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChapter([FromBody] ChapterDto chapter, int id)
        {
            await _service.Update(id, chapter);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ChapterDto chapter)
        {
            await _service.Delete(chapter);
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
