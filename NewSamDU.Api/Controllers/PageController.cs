using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.DTOs.PagesDTO;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Responses;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        protected readonly PageRepository pageRepository;

        protected readonly IMapper mapper;

        public PageController(PageRepository pageRepository, IMapper mapper)
        {
            this.pageRepository = pageRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslatedPage(int id)
        {
            var lang = CultureInfo.CurrentCulture.ToString();
            PageDTO? page = await pageRepository.GetAsync(id, lang);

            if (page is null)
            {
                return NotFound();
            }
            return Ok(page);
        }

        [HttpGet("{id}/full")]
        public async Task<IActionResult> GetFullPage(int id)
        {
            var lang = CultureInfo.CurrentCulture.ToString();
            Page? page = await pageRepository.GetAsync(id);

            if (page is null)
            {
                return NotFound();
            }
            return Ok(page);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePageDTO dto)
        {
            var page = mapper.Map<Page>(dto);

            var createdPage = await pageRepository.CreateAsync(page);
            return Ok(new Response<Page>(createdPage));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdatePageDTO dto)
        {
            var page = await pageRepository.GetAsync(id);

            if (page is null)
            {
                return NotFound();
            }

            mapper.Map(dto, page);

            await pageRepository.UpdateAsync(page);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var page = await pageRepository.GetAsync(id);

            if (page is null)
            {
                return NotFound();
            }
            await pageRepository.DeleteAsync(page);
            return Ok();
        }
    }
}

