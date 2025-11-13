using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.FileDTO;

public class DeleteFileDTO
{
    [Required]
    public string Path { get; set; } = string.Empty;
}
