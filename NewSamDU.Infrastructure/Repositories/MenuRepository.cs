using System;
using System.Linq.Expressions;
using System.Net.Quic;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.DTOs.Queries;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class MenuRepository : BaseRepository<Menu>
{
    public MenuRepository(AppDbContext context)
        : base(context) { }

    public override async Task<PaginatedResult<Menu>> GetAllAsync(int page = 1, int pageSize = 50)
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<Menu>(items, total, pageSize, page);
    }

    public async Task<List<Menu>> GetAllAsync(MenuQuery menuQuery)
    {
        int depth = menuQuery.Depth;
        var query = set.Where(m => m.DeletedAt == null && m.ParentId == null);

        query = depth switch
        {
            1 => query.Include(m =>
                m.Children.OrderBy(m => m.Priority)
                    .ThenByDescending(m => m.CreatedAt)
                    .Where(m => m.DeletedAt == null)
            ),

            2 => query
                .Include(m =>
                    m.Children.OrderBy(m => m.Priority)
                        .ThenByDescending(m => m.CreatedAt)
                        .Where(m => m.DeletedAt == null)
                )
                .ThenInclude(m =>
                    m.Children.OrderBy(m => m.Priority)
                        .ThenByDescending(m => m.CreatedAt)
                        .Where(m => m.DeletedAt == null)
                ),
            _ => query
                .Include(m =>
                    m.Children.OrderBy(m => m.Priority)
                        .ThenByDescending(m => m.CreatedAt)
                        .Where(m => m.DeletedAt == null)
                )
                .ThenInclude(m =>
                    m.Children.OrderBy(m => m.Priority)
                        .ThenByDescending(m => m.CreatedAt)
                        .Where(m => m.DeletedAt == null)
                )
                .ThenInclude(m =>
                    m.Children.OrderBy(m => m.Priority)
                        .ThenByDescending(m => m.CreatedAt)
                        .Where(m => m.DeletedAt == null)
                ),
        };

        return await query
            .OrderBy(m => m.Priority)
            .ThenByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    //     public async Task<List<Menu>> GetAllAsync_IterativeIncludes(MenuQuery menuQuery)
    //     {
    //         var query = set.Where(m => m.DeletedAt == null && m.ParentId == null);

    //         if (menuQuery.Depth > 0)
    //         {
    //             // Birinchi Include ni qo'llash
    //             var includeQuery = query.Include(m =>
    //                 m.Children.OrderBy(c => c.Priority)
    //                     .ThenByDescending(c => c.CreatedAt)
    //                     .Where(c => c.DeletedAt == null)
    //             );
    //         }

    //         return await query
    //             .OrderBy(m => m.Priority)
    //             .ThenByDescending(m => m.CreatedAt)
    //             .ToListAsync();
    //     }
}
