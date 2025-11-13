using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.DTOs.PagesDTO;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class PageRepository : BaseRepository<Page>
{
    public PageRepository(AppDbContext context)
        : base(context) { }

    public async Task<PageDTO?> GetAsync(int id, string lang)
    {
        var result = await set.Select(n => new PageDTO
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
            })
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();

        return result;
    }
}
