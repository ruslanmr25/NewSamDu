using System;
using NewSamDU.Application.Abstractions.IServices;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Infrastructure.Services;

public class ValidationService : IValidationService
{
    private readonly AppDbContext _context;

    public ValidationService(AppDbContext context)
    {
        _context = context;
    }

    public bool Exists<TEntity>(int id)
        where TEntity : BaseEntity
    {
        return _context.Set<TEntity>().Any(e => e.Id == id);
    }

    //     public bool IsUnique<TEntity>(int id)
    //         where TEntity : BaseEntity
    //     {
    //         throw new NotImplementedException();
    //     }
}
