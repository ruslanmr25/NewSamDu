using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewSamDU.Application.Results;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Repositories;

public class BaseRepository<T>
    where T : BaseEntity
{
    protected readonly AppDbContext context;

    protected readonly DbSet<T> set;

    public BaseRepository(AppDbContext context)
    {
        this.context = context;
        set = context.Set<T>();
    }

    protected IQueryable<T> BuildBaseQuery(
        Expression<Func<T, object>>? orderBy = null,
        bool descending = true
    )
    {
        IQueryable<T> query = context.Set<T>().AsQueryable();
        if (orderBy is not null)
        {
            query = descending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        }
        else
        {
            query = query.OrderByDescending(e => e.CreatedAt);
        }

        query = query.Where(e => e.DeletedAt == null);

        return query;
    }

    public virtual async Task<PaginatedResult<T>> GetAllAsync(int page = 1, int pageSize = 50)
    {
        var query = BuildBaseQuery();
        var total = await set.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<T>(items, total, pageSize, page);
    }

    public async Task<T> CreateAsync(T entity)
    {
        var result = await set.AddAsync(entity);

        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = set.Update(entity);

        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<T?> GetAsync(int Id)
    {
        var result = await set.FindAsync(Id);

        return result;
    }

    public async Task DeleteAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is BaseEntity en)
        {
            en.DeletedAt = DateTime.UtcNow;
            context.Update(en);
            await context.SaveChangesAsync();
        }
    }
}
