using System;

namespace NewSamDU.Application.DTOs.NewsDTOs;

public class NewsDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string MainImagePath { get; set; } = string.Empty;

    public int Views { get; set; }

    public int Likes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
