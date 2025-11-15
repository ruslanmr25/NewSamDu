using System;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.DTOs.MenuDTO;

public class MenuDTO
{
    public string Name { get; set; } = string.Empty;

    public int Priority { get; set; } = 1;

    public Menu? Parent { get; set; }

    public Menu? Child { get; set; }
    public int? ParentId { get; set; }

    public Page? RelatedPage { get; set; }

    public int? RelatedPageId { get; set; }

    public string? ExternalLink { get; set; }
}
