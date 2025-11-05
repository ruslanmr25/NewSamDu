using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.FolderDTO;

public class DeleteFolderDTO
{
    [Required]
    [StringLength(1000)]
    public string Path { get; set; } = string.Empty;
}
