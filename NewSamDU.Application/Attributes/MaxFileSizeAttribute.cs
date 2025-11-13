using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NewSamDU.Application.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeInBytes;

        /// <summary>
        /// Max file size in kilobytes
        /// </summary>
        /// <param name="sizeInKb"></param>
        public MaxFileSizeAttribute(int sizeInKb = 2048)
        {
            _maxFileSizeInBytes = sizeInKb * 1024;
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSizeInBytes)
                {
                    return new ValidationResult(
                        $"{validationContext.DisplayName} hajmi {_maxFileSizeInBytes / 1024} KB dan oshmasligi kerak."
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}
