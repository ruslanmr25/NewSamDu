using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NewSamDU.Application.Attributes;

namespace NewSamDU.Application.DTOs.FileDTO;

public class CreateFileDTO
{
    [Required]
    [AllowedFileExtension(
        [".doc", ".docx", ".xlsx", ".jpg", ".jpeg", ".png", ".webp", ".pdf", ".ppt"]
    )]
    [MaxFileSize(10240)]
    public IFormFile File { get; set; } = default!;

    public string Path { get; set; } = "/";
}
