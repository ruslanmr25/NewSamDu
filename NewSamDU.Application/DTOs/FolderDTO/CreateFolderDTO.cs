using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.FolderDTO;

public class CreateFolderDTO
{
    [Required(ErrorMessage = "Path maydoni majburiy.")]
    [StringLength(255, ErrorMessage = "Path uzunligi 255 belgidan oshmasligi kerak.")]
    public string Path { get; set; } = string.Empty;

    [Required(ErrorMessage = "Folder nomini kiritish shart.")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "Folder nomi 2 dan 100 belgigacha bo‘lishi kerak."
    )]
    public string FolderName { get; set; } = string.Empty;
}
