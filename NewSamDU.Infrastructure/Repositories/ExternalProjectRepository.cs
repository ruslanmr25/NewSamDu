using System;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class ExternalProjectRepository : BaseRepository<ExternalProject>
{
    public ExternalProjectRepository(AppDbContext context)
        : base(context) { }

    public override async Task<PaginatedResult<ExternalProject>> GetAllAsync(
        int page = 1,
        int pageSize = 50
    )
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query
            .Select(p => new ExternalProject
            {
                Id = p.Id,
                Title = p.Title,
                ProjectPath = p.ProjectPath,
                IsActive = p.IsActive,
                Owner = p.Owner,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                DeletedAt = p.DeletedAt,
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<ExternalProject>(items, total, pageSize, page);
    }
}
