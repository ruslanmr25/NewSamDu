using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.DTOs.PagesDTO;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Responses;
using NewSamDU.Domain.Entities;
using NewSamDU.Domain.Enums;
using NewSamDU.Infrastructure.Repositories;

namespace NewSamDU.Api.Controllers;

[Route("api/pages")]
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

    [HttpGet("/unassigned")]
    public async Task<IActionResult> UnassignedPages()
    {
        var pages = await pageRepository.GetUnassignedPages();

        return Ok(new Response<List<Page>>(pages));
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
    [Authorize(Roles = "Admin,Manager")]
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
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateAsync(CreatePageDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var page = mapper.Map<Page>(dto);

        page.OwnerId = userId;

        var createdPage = await pageRepository.CreateAsync(page);
        return Ok(new Response<Page>(createdPage));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatePageDTO dto)
    {
        var page = await pageRepository.GetAsync(id);

        if (page is null)
        {
            return NotFound();
        }

        mapper.Map(dto, page);

        var entity = await pageRepository.UpdateAsync(page);

        return Ok(new Response<Page>(entity));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var page = await pageRepository.GetAsync(id);

        if (page is null)
        {
            return NotFound();
        }
        await pageRepository.DeleteAsync(page);
        return Ok(new Response());
    }
}
