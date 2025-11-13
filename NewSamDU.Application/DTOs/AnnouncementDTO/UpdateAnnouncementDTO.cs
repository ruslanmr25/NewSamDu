using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.AnnouncementDTO;

public class UpdateAnnouncementDTO
{
    // [Required(ErrorMessage = "Title (Uz) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Uz) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleUz { get; set; }

    // [Required(ErrorMessage = "Title (En) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (En) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleEn { get; set; }

    // [Required(ErrorMessage = "Title (Ru) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Ru) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleRu { get; set; }

    // [Required(ErrorMessage = "Title (Kr) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Kr) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleKr { get; set; }

    public bool IsActive { get; set; }

    // [Required(ErrorMessage = "Description (Uz) kiritilishi shart.")]
    [StringLength(2000, ErrorMessage = "Description (Uz) 2000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionUz { get; set; }

    [StringLength(2000, ErrorMessage = "Description (Ru) 2000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionRu { get; set; }

    [StringLength(2000, ErrorMessage = "Description (En) 2000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionEn { get; set; }

    [StringLength(2000, ErrorMessage = "Description (Kr) 2000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionKr { get; set; }

    public string? ContentUz { get; set; }
    public string? ContentRu { get; set; }
    public string? ContentEn { get; set; }
    public string? ContentKr { get; set; }

    // [Required(ErrorMessage = "Asosiy rasm yo‘li (MainImagePath) kiritilishi shart.")]
    public string? MainImagePath { get; set; }
}
