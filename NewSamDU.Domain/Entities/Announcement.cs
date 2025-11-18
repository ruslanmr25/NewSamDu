using System;

namespace NewSamDU.Domain.Entities;

public class Announcement : BaseEntity
{
    public string? TitleUz { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;

    public string? TitleRu { get; set; } = string.Empty;

    public string? TitleKr { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public string DescriptionUz { get; set; } = string.Empty;
    public string? DescriptionRu { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;

    public string? DescriptionKr { get; set; } = string.Empty;

    public string? ContentUz { get; set; } = string.Empty;
    public string? ContentRu { get; set; } = string.Empty;
    public string? ContentEn { get; set; } = string.Empty;

    public string? ContentKr { get; set; } = string.Empty;

    public string MainImagePath { get; set; } = string.Empty;

    public int OwnerId { get; set; }

    public User Owner { get; set; } = default!;
}
