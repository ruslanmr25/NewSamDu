using System;

namespace NewSamDU.Domain.Entities;

public class Slide : BaseEntity
{
    public string? TitleUz { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;

    public string? TitleRu { get; set; } = string.Empty;

    public string? TitleKr { get; set; } = string.Empty;

    public string? DescriptionUz { get; set; } = string.Empty;
    public string? DescriptionRu { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public string? DescriptionKr { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    public Page? RelatedPage { get; set; }

    public int? RelatedPageId { get; set; }

    public string MainImagePath { get; set; } = string.Empty;
}
