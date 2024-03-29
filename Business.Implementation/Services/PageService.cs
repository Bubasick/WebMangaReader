﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Business.Abstraction;
using Business.Models.DTO;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace Business.Implementation.Services
{
    public class PageService :IPageService
    {
        private readonly MangaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;

        public PageService(MangaDbContext context, IMapper mapper, IBlobService service)
        {
            _dbContext = context;
            _mapper = mapper;
            _blobService = service;
        }

        public async Task<PageDto> GetById(int id)
        {
            var page =  await _dbContext.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if(page is null) throw new ArgumentException($"{nameof(id)} is not valid");
            var pageDto = new PageDto();
            _mapper.Map(page, pageDto);
            pageDto.BlobInfo = await _blobService.GetBlobAsync(pageDto.Guid.ToString());
          return pageDto;

        }
        public async Task<IEnumerable<string>> GetAllByChapterId(int chapterId)
        {
            List<PageDto> result = new List<PageDto>();
            var pages = await _dbContext.Pages.Where(x => x.ChapterId == chapterId).ToListAsync();
            var links = pages.Select(x => "https://localhost:44325/api/page/" + $"{x.Id}");
            return links;
        }

        public async Task Add(PageDto pageDto)
        {
            var fileName = Guid.NewGuid();
            pageDto.Guid = fileName;
            await _blobService.UploadContentBlobAsync(pageDto.Content, fileName); 
            await _dbContext.Pages.AddAsync(_mapper.Map<PageDto, Page>(pageDto));
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            var fileName = Guid.NewGuid();
            var page = _dbContext.Pages.FirstOrDefault(x => x.Id == id);
            if (page is null) throw new ArgumentException($"{nameof(id)} is not valid");
            await _blobService.DeleteBlobAsync(page.Guid.ToString());
            _dbContext.Pages.Remove(page);
            await _dbContext.SaveChangesAsync();
        }
    }
}