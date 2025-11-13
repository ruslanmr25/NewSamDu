using System;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.DTOs.SlideDTO;

public class SlideDTO
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public Page? RelatedPage { get; set; }

    public string MainImagePath { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
