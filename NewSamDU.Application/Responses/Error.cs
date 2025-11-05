using System;

namespace NewSamDU.Application.Responses;

public class Error
{
    public Dictionary<string, List<string>> Err { get; set; } = default!;
}
