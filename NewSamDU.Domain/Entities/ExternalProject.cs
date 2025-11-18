using System;

namespace NewSamDU.Domain.Entities;

public class ExternalProject : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string ProjectPath { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    public string StaticFilesPath { get; set; } = string.Empty;

    public User Owner { get; set; } = default!;

    public int OwnerId { get; set; }
}
