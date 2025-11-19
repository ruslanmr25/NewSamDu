using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.DTOs.SlideDTO;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace SlideamDU.Api.Controllers
{
    [Route("api/slides")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        protected readonly SlideRepository slideRepository;

        protected readonly IMapper mapper;

        public SlideController(SlideRepository slideRepository, IMapper mapper)
        {
            this.slideRepository = slideRepository;
            this.mapper = mapper;
        }

        [HttpGet("full")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseQuery query)
        {
            var items = await slideRepository.GetAllAsync(query);
            return Ok(new Response<PaginatedResult<Slide>>(items));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByLang([FromQuery] BaseQuery query)
        {
            var lang = CultureInfo.CurrentCulture.ToString();

            var items = await slideRepository.GetAllAsync(lang, query.Page, query.PageSize);
            return Ok(new Response<PaginatedResult<SlideDTO>>(items));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateAsync(CreateSlideDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            Slide slide = mapper.Map<Slide>(dto);
            slide.OwnerId = userId;

            var createdSlide = await slideRepository.CreateAsync(slide);
            return Ok(new Response<Slide>(createdSlide));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateSlideDTO dto)
        {
            var slide = await slideRepository.GetAsync(id);

            if (slide is null)
            {
                return NotFound();
            }

            mapper.Map(dto, slide);

            var entity = await slideRepository.UpdateAsync(slide);

            return Ok(new Response<Slide>(entity));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var slide = await slideRepository.GetAsync(id);

            if (slide is null)
            {
                return NotFound();
            }
            await slideRepository.DeleteAsync(slide);
            return Ok(new Response());
        }
    }
}
