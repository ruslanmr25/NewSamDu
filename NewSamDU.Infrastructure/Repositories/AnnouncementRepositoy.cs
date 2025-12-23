using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.AnnouncementDTO;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class AnnouncementRepositoy : BaseRepository<Announcement>
{
    public AnnouncementRepositoy(AppDbContext context)
        : base(context) { }

    public async Task<PaginatedResult<AnnouncementDTO>> GetAllAsync(
        string lang,
        int page = 1,
        int pageSize = 50
    )
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(n => new AnnouncementDTO
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
                CreatedAt = n.CreatedAt,
                MainImagePath = n.MainImagePath,

                UpdatedAt = n.UpdatedAt,
            })
            .ToListAsync();

        return new PaginatedResult<AnnouncementDTO>(items, total, pageSize, page);
    }

    public override async Task<PaginatedResult<Announcement>> GetAllAsync(
        int page = 1,
        int pageSize = 50
    )
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(n => new Announcement
            {
                Id = n.Id,
                TitleUz = n.TitleUz,
                TitleEn = n.TitleEn,
                TitleRu = n.TitleRu,
                TitleKr = n.TitleKr,

                DescriptionUz = n.DescriptionUz,
                DescriptionRu = n.DescriptionRu,
                DescriptionEn = n.DescriptionEn,

                DescriptionKr = n.DescriptionKr,

                MainImagePath = n.MainImagePath,

                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt,
            })
            .ToListAsync();

        return new PaginatedResult<Announcement>(items, total, pageSize, page);
    }

    public async Task<AnnouncementDTO?> GetAsync(int id, string lang)
    {
        var n = await GetAsync(id);

        if (n is null)
        {
            return null;
        }

        AnnouncementDTO dto = new()
        {
            Id = n.Id,

            Title =
                (
                    lang == "en" ? n.TitleEn
                    : lang == "ru" ? n.TitleRu
                    : lang == "kr" ? n.TitleKr
                    : n.TitleUz
                ) ?? n.TitleUz!,
            MainImagePath = n.MainImagePath,

            Description =
                (
                    lang == "en" ? n.DescriptionEn
                    : lang == "ru" ? n.DescriptionRu
                    : lang == "kr" ? n.DescriptionKr
                    : n.DescriptionUz
                ) ?? n.ContentUz!,

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

        return dto;
    }
}
