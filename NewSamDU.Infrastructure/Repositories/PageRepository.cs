using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.PagesDTO;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class PageRepository : BaseRepository<Page>
{
    public PageRepository(AppDbContext context)
        : base(context) { }

    public async Task<List<Page>> GetUnassignedPages()
    {
        var query = BuildBaseQuery();

        var pages = await query
            .Where(p => p.Menu == null)
            .Select(p => new Page
            {
                Id = p.Id,
                TitleUz = p.TitleUz,
                TitleEn = p.TitleEn,
                TitleKr = p.TitleKr,

                TitleRu = p.TitleRu,
            })
            .ToListAsync();

        return pages;
    }

    public async Task<PageDTO?> GetAsync(int id, string lang)
    {
        var n = await base.GetAsync(id);

        if (n is null)
        {
            return null;
        }
        var result = new PageDTO
        {
            Id = n.Id,
            Title =
                (
                    lang == "en" ? n.TitleEn
                    : lang == "ru" ? n.TitleRu
                    : lang == "kr" ? n.TitleKr
                    : n.TitleUz
                ) ?? n.TitleUz!,

            Content =
                (
                    lang == "en" ? n.ContentEn
                    : lang == "ru" ? n.ContentRu
                    : lang == "kr" ? n.ContentKr
                    : n.ContentEn
                ) ?? n.ContentUz!,
            CreatedAt = n.CreatedAt,
            UpdatedAt = n.UpdatedAt,
        };

        return result;
    }
}
