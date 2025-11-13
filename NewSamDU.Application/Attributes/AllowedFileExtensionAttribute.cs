using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NewSamDU.Application.Attributes;

public class AllowedFileExtensionAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedFileExtensionAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"{nameof(extension)} not allowed");
            }
        }
        else
        {
            return new ValidationResult($"{nameof(value)} is not file");
        }
        return ValidationResult.Success;
    }
}
