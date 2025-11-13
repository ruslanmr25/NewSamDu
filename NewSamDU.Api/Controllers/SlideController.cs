using System.Globalization;
using AutoMapper;
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
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseQuery query)
        {
            var items = await slideRepository.GetAllAsync(query.Page, query.PageSize);
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
        public async Task<IActionResult> CreateAsync(CreateSlideDTO dto)
        {
            var Slide = mapper.Map<Slide>(dto);

            var createdSlide = await slideRepository.CreateAsync(Slide);
            return Ok(new Response<Slide>(createdSlide));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateSlideDTO dto)
        {
            var Slide = await slideRepository.GetAsync(id);

            if (Slide is null)
            {
                return NotFound();
            }

            mapper.Map(dto, Slide);

            // await slideRepository.UpdateAsync(Slide);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var Slide = await slideRepository.GetAsync(id);

            if (Slide is null)
            {
                return NotFound();
            }
            await slideRepository.DeleteAsync(Slide);
            return Ok();
        }
    }
}
