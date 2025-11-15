using System;
using System.ComponentModel.DataAnnotations;
using NewSamDU.Application.Attributes;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.DTOs.SlideDTO;

public class UpdateSlideDTO
{
    [StringLength(200, ErrorMessage = "Title (Uz) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleUz { get; set; }

    [StringLength(200, ErrorMessage = "Title (En) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleEn { get; set; }

    [StringLength(200, ErrorMessage = "Title (Ru) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleRu { get; set; }

    [StringLength(200, ErrorMessage = "Title (Kr) 200 ta belgidan oshmasligi kerak.")]
    public string? TitleKr { get; set; }

    [StringLength(1000, ErrorMessage = "Description (Uz) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionUz { get; set; }

    [StringLength(1000, ErrorMessage = "Description (Ru) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionRu { get; set; }

    [StringLength(1000, ErrorMessage = "Description (En) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionEn { get; set; }

    [StringLength(1000, ErrorMessage = "Description (Kr) 1000 ta belgidan oshmasligi kerak.")]
    public string? DescriptionKr { get; set; }

    [Range(0, int.MaxValue)]
    [Exists<Page>]
    public int? RelatedPageId { get; set; }

    [StringLength(300, ErrorMessage = "Rasm yo‘li 300 ta belgidan oshmasligi kerak.")]
    public string? MainImagePath { get; set; }

    public bool? IsActive { get; set; }
}
