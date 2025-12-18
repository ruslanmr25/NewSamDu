using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.NewsDTOs;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class NewsRepository : BaseRepository<News>
{
    public NewsRepository(AppDbContext context)
        : base(context) { }

    public async Task<PaginatedResult<NewsDTO>> GetAllAsync(
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
            .Select(n => new NewsDTO
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
                UpdatedAt = n.UpdatedAt,
            })
            .ToListAsync();

        return new PaginatedResult<NewsDTO>(items, total, pageSize, page);
    }

    public override async Task<PaginatedResult<News>> GetAllAsync(int page = 1, int pageSize = 50)
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(n => new News
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
                Likes = n.Likes,
                Views = n.Views,

                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt,
            })
            .ToListAsync();

        return new PaginatedResult<News>(items, total, pageSize, page);
    }

    public async Task<NewsDTO?> GetAsync(int id, string lang)
    {
        var n = await GetAsync(id);

        if (n is null)
        {
            return null;
        }
        var result = new NewsDTO
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
                ) ?? n.ContentUz!,

            Content =
                (
                    lang == "en" ? n.ContentEn
                    : lang == "ru" ? n.ContentRu
                    : lang == "kr" ? n.ContentKr
                    : n.ContentUz
                ) ?? n.ContentUz!,
            CreatedAt = n.CreatedAt,
            MainImagePath = n.MainImagePath,
            UpdatedAt = n.UpdatedAt,
        };

        return result;
    }
}
