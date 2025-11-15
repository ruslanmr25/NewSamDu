using System;
using System.ComponentModel.DataAnnotations;
using NewSamDU.Application.Abstractions.IServices;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.Attributes;

public class ExistsAttribute<TEntity>() : ValidationAttribute
    where TEntity : BaseEntity
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }
        if (value is int id)
        {
            var service =
                (IValidationService?)validationContext.GetService(typeof(IValidationService))
                ?? throw new Exception("IValidationService is not registered");

            bool exists = service.Exists<TEntity>(id);
            return exists
                ? ValidationResult.Success
                : new ValidationResult($"Tanlangan Id {id} ga mos ma'lumot bazadan topilmadi");
        }

        return new ValidationResult($"Tanlangan Id ga mos ma'lumot bazadan topilmadi");
    }
}
