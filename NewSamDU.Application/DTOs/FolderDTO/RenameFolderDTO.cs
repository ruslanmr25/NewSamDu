using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.FolderDTO;

public class RenameFolderDTO
{
    [Required(ErrorMessage = "Path maydoni majburiy.")]
    [StringLength(255, ErrorMessage = "Path uzunligi 255 belgidan oshmasligi kerak.")]
    public string Path { get; set; } = string.Empty;

    [Required(ErrorMessage = "Folder nomini kiritish shart.")]
    [StringLength(255)]
    public string OldName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yangi  nom kiritish shart.")]
    [StringLength(255)]
    public string NewName { get; set; } = string.Empty;
}
