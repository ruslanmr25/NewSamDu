using System;
using System.ComponentModel.DataAnnotations;
using NewSamDU.Application.Attributes;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.DTOs.MenuDTO;

public class UpdateMenuDTO
{
    [StringLength(100, ErrorMessage = "NameUz 100 ta belgidan oshmasligi kerak.")]
    public string? NameUz { get; set; }

    [StringLength(100, ErrorMessage = "NameRu 100 ta belgidan oshmasligi kerak.")]
    public string? NameRu { get; set; }

    [StringLength(100, ErrorMessage = "NameEn 100 ta belgidan oshmasligi kerak.")]
    public string? NameEn { get; set; }

    [StringLength(100, ErrorMessage = "NameKr 100 ta belgidan oshmasligi kerak.")]
    public string? NameKr { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Priority 1 dan kichik bo‘lishi mumkin emas.")]
    public int Priority { get; set; } = 1;

    [Range(1, int.MaxValue)]
    [Exists<Page>]
    public int? ParentId { get; set; }

    [Range(1, int.MaxValue)]
    [Exists<Page>()]
    public int? RelatedPageId { get; set; }

    [Url(ErrorMessage = "ExternalLink to‘g‘ri URL formatida bo‘lishi kerak.")]
    [StringLength(255, ErrorMessage = "ExternalLink 255 ta belgidan oshmasligi kerak.")]
    public string? ExternalLink { get; set; }
}
