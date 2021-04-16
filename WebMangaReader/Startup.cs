using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Business.Abstraction;
using Business.Implementation;
using Business.Implementation.Services;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace WebMangaReader
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));
            services.AddSingleton<IBlobService,BlobService>();
            services.AddDbContext<MangaDbContext>(x =>  x.UseNpgsql(Configuration.GetValue<string>("DefaultConnection")));
            services.AddTransient<IMangaService, MangaService>();
            services.AddTransient<IChapterService, ChapterService>();
            services.AddTransient<IPageService, PageService>();
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
