using System;
using System.ComponentModel.DataAnnotations;

namespace NewSamDU.Application.DTOs.SlideDTO;

public class UpdateSlideDTO
{
    [Required(ErrorMessage = "Title (Uz) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Uz) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleUz { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title (En) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (En) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleEn { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title (Ru) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Ru) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleRu { get; set; } = string.Empty;

    [Required(ErrorMessage = "Title (Kr) kiritilishi shart.")]
    [StringLength(200, ErrorMessage = "Title (Kr) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleKr { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description (Uz) kiritilishi shart.")]
    [StringLength(1000, ErrorMessage = "Description (Uz) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionUz { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description (Ru) kiritilishi shart.")]
    [StringLength(1000, ErrorMessage = "Description (Ru) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionRu { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description (En) kiritilishi shart.")]
    [StringLength(1000, ErrorMessage = "Description (En) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionEn { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description (Kr) kiritilishi shart.")]
    [StringLength(1000, ErrorMessage = "Description (Kr) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionKr { get; set; } = string.Empty;

    public int? RelatedPageId { get; set; }

    [Required(ErrorMessage = "Asosiy rasm yo‘li (MainImagePath) kiritilishi shart.")]
    [StringLength(300, ErrorMessage = "Rasm yo‘li 300 ta belgidan oshmasligi kerak.")]
    public string MainImagePath { get; set; } = string.Empty;
}
