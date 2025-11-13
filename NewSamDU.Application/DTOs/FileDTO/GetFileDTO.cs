using System;

namespace NewSamDU.Application.DTOs.FileDTO;

public class GetFileDTO
{
    public string Name { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Url { get; set; } = string.Empty;
}
