using System;

namespace NewSamDU.Domain.Entities;

public class Page : BaseEntity
{
    public string TitleUz { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;

    public string TitleRu { get; set; } = string.Empty;

    public string TitleKr { get; set; } = string.Empty;

    public string ContentUz { get; set; } = string.Empty;
    public string ContentRu { get; set; } = string.Empty;
    public string ContentEn { get; set; } = string.Empty;

    public string ContentKr { get; set; } = string.Empty;
}
