using System;

namespace NewSamDU.Application.DTOs.ProjectsDTO;

public class CreateProjectDTO
{
    public string Title { get; set; } = string.Empty;

    public string ProjectPath { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsActive { get; set; } = false;

    public string StaticFilesPath { get; set; } = string.Empty;
}
