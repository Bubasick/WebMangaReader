using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMangaReader.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Eventing.Reader;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Business.Abstraction;
    using Business.Models.DTO;

    namespace WebMangaReader.Controllers
    {
        [Route("api/[controller]")]
        public class PageController : Controller
        {
            private readonly IPageService _service;

            public PageController(IPageService service)
            {
                _service = service;
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var result = await _service.GetById(id);
                return File(result.BlobInfo.Content, result.BlobInfo.ContentType);
            }

            [HttpGet("chapterId/{chapterId}")]
            public async Task<IActionResult> GetAllByChapterId(int chapterId)
            {
                var result = await _service.GetAllByChapterId(chapterId);
                return Ok(result);
            }
            [HttpPost]
            public async Task<IActionResult> Add([FromForm] PageDto pageDto)
            {

                await _service.Add(pageDto);
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
}
