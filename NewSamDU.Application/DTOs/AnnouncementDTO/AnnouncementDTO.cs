using System;
using NewSamDU.Domain.Entities;

namespace NewSamDU.Application.DTOs.AnnouncementDTO;

public class AnnouncementDTO
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public string Description { get; set; } = string.Empty;

    public string? Content { get; set; } = string.Empty;

    public string MainImagePath { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
