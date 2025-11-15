using System;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.Abstractions.IServices;

public interface IValidationService
{
    //     public bool IsUnique<TEntity>(int id)
    //         where TEntity : BaseEntity;

    public bool Exists<TEntity>(int id)
        where TEntity : BaseEntity;
}
