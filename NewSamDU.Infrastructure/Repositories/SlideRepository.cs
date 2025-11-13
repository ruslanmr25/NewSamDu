using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.SlideDTO;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class SlideRepository : BaseRepository<Slide>
{
    public SlideRepository(AppDbContext context)
        : base(context) { }

    public async Task<PaginatedResult<SlideDTO>> GetAllAsync(
        string lang,
        int page = 1,
        int pageSize = 50
    )
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query
            .Where(s => s.IsActive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(n => new SlideDTO
            {
                Id = n.Id,

                Title =
                    (
                        lang == "en" ? n.TitleEn
                        : lang == "ru" ? n.TitleRu
                        : lang == "kr" ? n.TitleKr
                        : n.TitleUz
                    ) ?? n.TitleUz!,

                Description =
                    (
                        lang == "en" ? n.DescriptionEn
                        : lang == "ru" ? n.DescriptionRu
                        : lang == "kr" ? n.DescriptionKr
                        : n.DescriptionUz
                    ) ?? n.DescriptionUz!,

                RelatedPage = n.RelatedPage,

                MainImagePath = n.MainImagePath,

                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt,

                IsActive = n.IsActive,
            })
            .ToListAsync();

        return new PaginatedResult<SlideDTO>(items, total, pageSize, page);
    }
}
