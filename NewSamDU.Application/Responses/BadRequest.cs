using System;

namespace NewSamDU.Application.Responses;

public class BadRequest(IEnumerable<object> errors)
{
    public bool Success { get; set; } = false;

    public string Message { get; set; } = "Bad request";

    public object? Data { get; set; }

    public IEnumerable<object> Errors { get; set; } = errors;
}
