using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Models.DTO;
using Data.Entities;
using Data.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Business.Implementation.Services
{
    public class ChapterService : IChapterService
    {
        private readonly MangaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPageService _pageService;

        public ChapterService(MangaDbContext context, IMapper mapper, IPageService pageService)
        {
            _dbContext = context;
            _mapper = mapper;
            _pageService = pageService;
        }

        public async Task<ChapterDto> GetById(int id)
        {
            var result = await _dbContext.Chapters.Include(p => p.Pages).FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                throw new ArgumentException($"{nameof(id)} is not valid.");
            }
            return _mapper.Map<Chapter, ChapterDto>(result);
        }

        public async Task<IEnumerable<ChapterDto>> GetAll()
        {
            var result = await _dbContext.Chapters.Include(p => p.Pages).ToListAsync();
            return _mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterDto>>(result);
        }


        public async Task<IEnumerable<string>> GetAllPagesLinksById(int id)
        {
            var chapter = await _dbContext.Chapters.Include(p => p.Pages).FirstOrDefaultAsync(x => x.Id == id);
            var result = chapter.Pages.Select(page => "https://localhost:44325/api/page/" + $"{page.Id}");
            return result;
        }

        public async Task Add(ChapterDto chapterDto)
        {
            if(chapterDto.PageArchive.ContentType != "application/zip") throw new ArgumentException("You must upload a zip file!");
            Stream stream = new MemoryStream();
            chapterDto.PageArchive.CopyTo(stream);
            var tempFolderName = Guid.NewGuid().ToString();
            //TODO: change paths to relative and stash them somewhere else
            string path = "../temp/" + tempFolderName + "/";
            await using (stream)
            {
                stream.Position = 0;
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    
                    Directory.CreateDirectory(path);
                    archive.ExtractToDirectory(path);
                }
            }
            await _dbContext.Chapters.AddAsync(_mapper.Map<ChapterDto, Chapter>(chapterDto));
            await _dbContext.SaveChangesAsync();
            int chapterId = await _dbContext.Chapters.OrderByDescending(x => x.Id).Select(x => x.Id)
                .FirstOrDefaultAsync();
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.jpeg"); //Getting jpg files
            foreach (FileInfo fileInfo in Files)
            {
                await using (FileStream file = File.OpenRead(path + fileInfo.Name))
                {
                    if(fileInfo.Extension!=".jpeg") throw new ArgumentException($"{fileInfo.Name} + must have an jpg extension!");
                    MemoryStream buffer =  new MemoryStream();
                    file.CopyTo(buffer);
                    buffer.Position = 0;
                    
                    IFormFile formFile = new FormFile(buffer,0,buffer.Length, fileInfo.Name,fileInfo.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg",
                        ContentDisposition = "form-data"
                    }; 
                    PageDto page = new PageDto();
                    page.ChapterId = chapterId;
                    page.Content = formFile;
                    page.Name = fileInfo.Name;
                   await _pageService.Add(page);
                }
            }
            Directory.Delete(path, true);
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
