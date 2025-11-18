using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.AutoMappers;
using NewSamDU.Application.DTOs.NewsDTOs;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        protected readonly NewsRepository newsRepository;

        protected readonly IMapper mapper;

        public NewsController(NewsRepository newsRepository, IMapper mapper)
        {
            this.newsRepository = newsRepository;
            this.mapper = mapper;
        }

        [HttpGet("full")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseQuery query)
        {
            var items = await newsRepository.GetAllAsync(query.Page, query.PageSize);
            return Ok(new Response<PaginatedResult<News>>(items));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByLang([FromQuery] BaseQuery query)
        {
            var lang = CultureInfo.CurrentCulture.ToString();

            var items = await newsRepository.GetAllAsync(lang, query.Page, query.PageSize);
            return Ok(new Response<PaginatedResult<NewsDTO>>(items));
        }

        [HttpGet("{id}/full")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetFullById(int id)
        {
            var news = await newsRepository.GetAsync(id);
            return Ok(new Response<News>(news));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslatedById(int id)
        {
            var lang = CultureInfo.CurrentCulture.ToString();
            var news = await newsRepository.GetAsync(id, lang);
            return Ok(new Response<NewsDTO>(news));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateAsync(CreateNewsDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var news = mapper.Map<News>(dto);

            news.OwnerId = userId;

            var createdNews = await newsRepository.CreateAsync(news);
            return Ok(new Response<News>(createdNews));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateNewsDTO dto)
        {
            var news = await newsRepository.GetAsync(id);

            if (news is null)
            {
                return NotFound();
            }

            mapper.Map(dto, news);

            var entity = await newsRepository.UpdateAsync(news);

            return Ok(new Response<News>(entity));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var news = await newsRepository.GetAsync(id);

            if (news is null)
            {
                return NotFound();
            }
            await newsRepository.DeleteAsync(news);
            return Ok(new Response());
        }
    }
}
