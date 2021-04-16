using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] PageDto pageDto)
        {
            await _service.Add(pageDto);
            return Ok();
        }
    }
}
