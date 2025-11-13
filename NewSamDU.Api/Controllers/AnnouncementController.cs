using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Responses;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers
{
    [Route("api/announcements")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        protected readonly AnnouncementRepositoy announcementRepository;

        protected readonly IMapper mapper;

        public AnnouncementController(AnnouncementRepositoy announcementRepository, IMapper mapper)
        {
            this.announcementRepository = announcementRepository;
            this.mapper = mapper;
        }

        [HttpGet("full")]
        public async Task<IActionResult> GetAllAsync([FromQuery] BaseQuery query)
        {
            var items = await announcementRepository.GetAllAsync(query.Page, query.PageSize);
            return Ok(new Response<PaginatedResult<Announcement>>(items));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByLang([FromQuery] BaseQuery query)
        {
            var lang = CultureInfo.CurrentCulture.ToString();

            var items = await announcementRepository.GetAllAsync(lang, query.Page, query.PageSize);
            return Ok(new Response<PaginatedResult<AnnouncementDTO>>(items));
        }

        [HttpGet("{id}/full")]
        public async Task<IActionResult> GetFullById(int id)
        {
            var announcement = await announcementRepository.GetAsync(id);

            if (announcement is null)
            {
                return NotFound();
            }
            return Ok(new Response<Announcement>(announcement));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslatedById(int id)
        {
            var lang = CultureInfo.CurrentCulture.ToString();
            var announcement = await announcementRepository.GetAsync(id, lang);

            if (announcement is null)
            {
                return NotFound();
            }
            return Ok(new Response<AnnouncementDTO>(announcement));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateAnnouncementDto dto)
        {
            var annoucement = mapper.Map<Announcement>(dto);

            var entity = await announcementRepository.CreateAsync(annoucement);
            return Ok(new Response<Announcement>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateAnnouncementDTO dto)
        {
            var announcement = await announcementRepository.GetAsync(id);

            if (announcement is null)
            {
                return NotFound();
            }

            mapper.Map(dto, announcement);

            await announcementRepository.UpdateAsync(announcement);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var announcement = await announcementRepository.GetAsync(id);

            if (announcement is null)
            {
                return NotFound();
            }
            await announcementRepository.DeleteAsync(announcement);
            return Ok();
        }
    }
}
